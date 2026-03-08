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
                var nature = await _natureRepository.GetNatureByIdAsync(category.NatureID);
                if(nature == null)
                    throw new ArgumentException("Debe de ingresar una naturaleza valida");

                //ver si el usuario existe
                var user = await _userRepository.GetByIdAsync(category.UserID);
                if (user == null)
                    throw new ArgumentException("Debe de ingresar un usuario valido");

                //ver si la categoria padre existe
                if(category.ParentID != null){
                    var parentCategory = await _categoryRepository.GetByIdAsync(category.ParentID ?? Guid.Empty);
                    if (parentCategory == null)
                        throw new ArgumentException("Debe de ingresar una categoria padre valida");
                }



                //agregar una nueva categoria
                var newCategory = new Category(category.UserID, category.NatureID, category.Name, category.Description, category.ParentID);
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
        public async Task UpdateCategoryAsync(CategoryDTO category)
        {
            if (string.IsNullOrWhiteSpace(category.Name))
                throw new ArgumentException("El nombre de la categoría no puede estar vacío.");
            if (string.IsNullOrEmpty(category.Description))
                throw new ArgumentException("Debe de ingresar una descripcion");
            

            var newCategory = new Category(category.UserID, category.NatureID, category.Name, category.Description, category.ParentID);


            await _categoryRepository.UpdateAsync(newCategory);
        }

        /// <summary>
        /// busca las categorias de un usuario
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<IEnumerable<CategoryDTO?>> GetCategoriesByUserIdAsync(Guid userId)
        {
            if (userId == Guid.Empty)
                throw new ArgumentException("El ID del usuario no puede ser vacío.");

            var categories = await _categoryRepository.GetByUserIdAsync(userId);

            return categories.Select(c => c == null ? null : new CategoryDTO
            {
                CategoryID = c.Id,
                UserID = c.UserID,
                ParentID = c.ParentID,
                NatureID = c.NatureID,
                Name = c.Name,
                Description = c.Description
            });
        }

        /// <summary>
        /// detalla la informacion de una categoria
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public async Task<CategoryDTO?> GetCategoryByIdAsync(Guid id)
        {
            if (id == Guid.Empty)
                throw new ArgumentException("El ID de la categoría debe ser un número positivo.");
            var category =  await _categoryRepository.GetByIdAsync(id);


            return category == null ? null : new CategoryDTO
            {
                CategoryID = category.Id,
                UserID = category.UserID,
                ParentID = category.ParentID,
                NatureID = category.NatureID,
                Name = category.Name,
                Description = category.Description
            };


        }
    }
}
