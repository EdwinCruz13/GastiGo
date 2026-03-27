using Application.Features.Finances.DTOs;
using Application.Features.Finances.Interfaces;
using Application.Features.Users.Interfaces;
using Domain.Features.Finances.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Reflection.Metadata;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Finances.Services
{
    public class CategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly INatureRepository _natureRepository;
        private readonly IUserRepository _userRepository;


        /// <summary>
        /// recibe parametros para inyeccion de dependencia
        /// </summary>
        /// <param name="categoryRepository"></param>
        public CategoryService(ICategoryRepository categoryRepository, INatureRepository natureRepository, IUserRepository userRepository)
        {
            _categoryRepository = categoryRepository;
            _natureRepository = natureRepository;
            _userRepository = userRepository;
        }

        /// <summary>
        /// crea una nueva categoría de finanzas, validando que el nombre no esté vacío y 
        /// luego simula una operación asíncrona para guardar la categoría en una base de datos.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="description"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public async Task CreateCategoryAsync(CategoryDTO category)
        {
            // Validar que el nombre no esté vacío
            try
            {
                if (string.IsNullOrWhiteSpace(category.Name))
                    throw new ArgumentException("El nombre de la categoría no puede estar vacío.");

                if (string.IsNullOrEmpty(category.Description))
                    throw new ArgumentException("Debe de ingresar una descripcion");


                //ver si la naturaleza existe
                var nature = await _natureRepository.GetNatureByIdAsync(category.NatureId);
                if (nature == null)
                    throw new ArgumentException("Debe de ingresar una naturaleza valida");

                //ver si el usuario existe
                var user = await _userRepository.GetUserByIdAsync(category.UserId);
                if (user == null)
                    throw new ArgumentException("Debe de ingresar un usuario valido");

                //ver si la categoria padre existe
                if (category.ParentId != null)
                {
                    var parentCategory = await _categoryRepository.GetCategoryByIdAsync(category.ParentId ?? Guid.Empty);
                    if (parentCategory == null)
                        throw new ArgumentException("Debe de ingresar una categoria padre valida");
                }



                //agregar una nueva categoria
                var newCategory = new Category(category.UserId, category.NatureId, category.Name, category.Description, category.ParentId, category.isSalary);
                var categoryParamsList = new List<CategoryParams>();


                //aplicar parametros a la categoria
                if (category.ApplySalary)
                    categoryParamsList.Add(new CategoryParams(newCategory.Id, category.ApplySalary, category.ApplyPercentage, category.ApplyAmount, category.Value));


                //si hay parametros para aplicar, se agregan a la categoria
                foreach (var param in categoryParamsList)
                    newCategory.Params.Add(param);



                await _categoryRepository.AddAsync(newCategory);
                await _categoryRepository.SaveChangesAsync(); //guardar 
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }

        }

        /// <summary>
        /// actualiza una categoría existente, validando que el nombre no esté vacío 
        /// </summary>
        /// <param name="category"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public async Task UpdateCategoryAsync(Guid id, CategoryDTO category)
        {

            //validar si la categoria existe
            var existingCategory = await _categoryRepository.GetCategoryByIdAsync(id);
            if (existingCategory == null)
                throw new ArgumentException("La categoría no existe.");

            if (string.IsNullOrWhiteSpace(category.Name))
                throw new ArgumentException("El nombre de la categoría no puede estar vacío.");
            if (string.IsNullOrEmpty(category.Description))
                throw new ArgumentException("Debe de ingresar una descripcion");
            if (category.ParentId == id)
                throw new ArgumentException("Una categoría no puede ser su propio padre");


            // actualizar propiedades
            existingCategory.Update(
                 category.UserId,
                 category.Name,
                 category.Description,
                 category.NatureId,
                 category.ParentId,
                 category.isActive,
                 category.isSalary
             );

            await _categoryRepository.SaveChangesAsync();
        }

        /// <summary>
        /// da de baja una categoría, validando que la categoría exista 
        /// no borra la categoría de la base de datos, sino que la marca como eliminada, lo que permite mantener un historial de categorías y evitar problemas de integridad referencial en caso de que otras entidades estén relacionadas con esa categoría.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public async Task DeleteCategoryAsync(Guid id)
        {
            //validar si la categoria existe
            var existingCategory = await _categoryRepository.GetCategoryByIdAsync(id);
            if (existingCategory == null)
                throw new ArgumentException("La categoría no existe.");

            //ver si tiene subcategorias
            var hasChildren = await _categoryRepository.HasChildrenAsync(id);

            //si tiene subcategorias, no se puede eliminar
            if (hasChildren)
                throw new InvalidOperationException("Debe eliminar primero las subcategorías.");

            //marcar como eliminada
            existingCategory.MarkAsDeleted();
            //guardar cambios
            await _categoryRepository.SaveChangesAsync();
        }

        /// <summary>
        /// busca las categorias de un usuario
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<IEnumerable<CategoryResponseDTO?>> GetCategoriesByUserIdAsync(Guid userId, bool flagTree = true)
        {
            if (userId == Guid.Empty)
                throw new ArgumentException("El ID del usuario no puede ser vacío.");

            var categories = await _categoryRepository.GetCategoryByUserIdAsync(userId);

            //si no se requiere el arbol de categorias, se devuelve la lista sin construir el árbol
            if (flagTree == false)
            {
                return categories.Select(x => new CategoryResponseDTO
                {
                    CategoryId = x.Id,
                    UserId = x.UserId,
                    ParentId = x.ParentId,
                    Nature = new NatureDTO { NatureId = x.Nature.Id, Name = x.Nature.Name, Abbre = x.Nature.Abbre },
                    Name = x.Name,
                    Description = x.Description,
                    isActive = x.isActive,
                    isSalary = x.isSalary,

                    ApplySalary = x.Params.Any(p => p.ApplySalary) ? x.Params.FirstOrDefault(p => p.ApplySalary)?.ApplySalary ?? false : false,
                    ApplyPercentage = x.Params.Any(p => p.ApplyPercentage) ? x.Params.FirstOrDefault(p => p.ApplyPercentage)?.ApplyPercentage ?? false : false,
                    ApplyAmount = x.Params.Any(p => p.ApplyAmount) ? x.Params.FirstOrDefault(p => p.ApplyAmount)?.ApplyAmount ?? false : false,
                    Value = x.Params.Any(p => p.ApplyAmount || p.ApplyPercentage || p.ApplySalary) ? x.Params.FirstOrDefault(p => p.ApplyAmount || p.ApplyPercentage || p.ApplySalary)?.Value ?? 0 : 0
                }).ToList();
            }

            // Construir el árbol de categorías a partir de la lista obtenida
            var tree = BuildTree(categories, null);

            // Devolver el árbol de categorías
            return tree;
        }


        /// <summary>
        /// detalla la informacion de una categoria
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public async Task<CategoryResponseDTO?> GetCategoryByIdAsync(Guid id)
        {
            if (id == Guid.Empty)
                throw new ArgumentException("El ID de la categoría debe ser un número positivo.");
            var category = await _categoryRepository.GetCategoryByIdAsync(id);


            return category == null ? null : new CategoryResponseDTO
            {
                CategoryId = category.Id,
                UserId = category.UserId,
                ParentId = category.ParentId,
                Nature = new NatureDTO { NatureId = category.Nature.Id, Name = category.Nature.Name, Abbre = category.Nature.Abbre },
                Name = category.Name,
                Description = category.Description,
                isActive = category.isActive,
                isSalary = category.isSalary
            };


        }


        /// <summary>
        /// funcion recursiva para construir un árbol de categorías a partir de una lista de categorías,
        /// </summary>
        /// <param name="categories"></param>
        /// <param name="parentId"></param>
        /// <returns></returns>
        private List<CategoryResponseDTO> BuildTree(List<Category> categories, Guid? parentId, int level = 0)
        {
            return categories
                .Where(x => x.ParentId == parentId)
                .Select(x => new CategoryResponseDTO
                {
                    CategoryId = x.Id,
                    UserId = x.UserId,
                    ParentId = x.ParentId,
                    Nature = new NatureDTO { NatureId = x.Nature.Id, Name = x.Nature.Name, Abbre = x.Nature.Abbre },
                    Name = x.Name,
                    Description = x.Description,
                    Level = level,
                    Children = BuildTree(categories, x.Id, level + 1),
                    isActive = x.isActive,
                    isSalary = x.isSalary,

                    ApplySalary = x.Params.Any(p => p.ApplySalary) ? x.Params.FirstOrDefault(p => p.ApplySalary)?.ApplySalary ?? false : false,
                    ApplyPercentage = x.Params.Any(p => p.ApplyPercentage) ? x.Params.FirstOrDefault(p => p.ApplyPercentage)?.ApplyPercentage ?? false : false,
                    ApplyAmount = x.Params.Any(p => p.ApplyAmount) ? x.Params.FirstOrDefault(p => p.ApplyAmount)?.ApplyAmount ?? false : false,
                    Value = x.Params.Any(p => p.ApplyAmount || p.ApplyPercentage || p.ApplySalary) ? x.Params.FirstOrDefault(p => p.ApplyAmount || p.ApplyPercentage || p.ApplySalary)?.Value ?? 0 : 0

                })
                .ToList();
        }
    }
}
