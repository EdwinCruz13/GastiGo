using Application.Features.Finances.DTOs;
using Application.Features.Finances.Interfaces;
using Application.Features.Users.Interfaces;
using Domain.Features.Finances.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
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
                if(nature == null)
                    throw new ArgumentException("Debe de ingresar una naturaleza valida");

                //ver si el usuario existe
                var user = await _userRepository.GetByIdAsync(category.UserId);
                if (user == null)
                    throw new ArgumentException("Debe de ingresar un usuario valido");

                //ver si la categoria padre existe
                if(category.ParentId != null){
                    var parentCategory = await _categoryRepository.GetByIdAsync(category.ParentId ?? Guid.Empty);
                    if (parentCategory == null)
                        throw new ArgumentException("Debe de ingresar una categoria padre valida");
                }



                //agregar una nueva categoria
                var newCategory = new Category(category.UserId, category.NatureId, category.Name, category.Description, category.ParentId);
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
            var existingCategory = await _categoryRepository.GetByIdAsync(id);
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
                 category.ParentId
             );

             await _categoryRepository.SaveChangesAsync();
        }

        /// <summary>
        /// busca las categorias de un usuario
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<IEnumerable<CategoryResponseDTO?>> GetCategoriesByUserIdAsync(Guid userId)
        {
            if (userId == Guid.Empty)
                throw new ArgumentException("El ID del usuario no puede ser vacío.");

            var categories = await _categoryRepository.GetByUserIdAsync(userId);

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
            var category =  await _categoryRepository.GetByIdAsync(id);


            return category == null ? null : new CategoryResponseDTO
            {
                CategoryId = category.Id,
                UserId = category.UserId,
                ParentId = category.ParentId,
                Nature = new NatureDTO { NatureId = category.Nature.Id, Name = category.Nature.Name, Abbre = category.Nature.Abbre },
                Name = category.Name,
                Description = category.Description
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
                    Children = BuildTree(categories, x.Id, level + 1)
                   
                })
                .ToList();
        }
    }
}
