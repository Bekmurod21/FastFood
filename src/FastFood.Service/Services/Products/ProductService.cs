﻿using AutoMapper;
using FastFood.Domain.Helpers;
using FastFood.Data.IRepositories;
using FastFood.Service.Exceptions;
using FastFood.Service.Extensions;
using Microsoft.EntityFrameworkCore;
using FastFood.Domain.Configurations;
using FastFood.Service.DTOs.ProductDto;
using FastFood.Domain.Entities.Products;
using FastFood.Service.Interfaces.Products;

namespace FastFood.Service.Services.Products
{
    public class ProductService : IProductService
    {
        private readonly IRepository<Product> productRepository;
        private readonly IMapper mapper;
        public ProductService(IRepository<Product> productRepository, IMapper mapper)
        {
            this.productRepository = productRepository;
            this.mapper = mapper;
        }

        public async ValueTask<ProductForResultDto> AddAsync(ProductForCreationDto model)
        {
            var entity = await productRepository.SelectAsync(x => x.Name == model.Name);
            if (entity is not null)
                throw new CustomException(405, "Product already exist");

            Product mappedProduct = mapper.Map<Product>(model);
            mappedProduct.CreatedAt = DateTime.Now;
            mappedProduct.CreatedBy = HttpContextHelper.UserId;

            try
            {
                var result = await productRepository.InsertAsync(mappedProduct);


                return mapper.Map<ProductForResultDto>(result);
            }

            catch (Exception)
            {
                throw new CustomException(500, "Something went wrong");
            }
        }

        public async ValueTask<bool> RemoveAsync(long id)
        {
            var entity = await productRepository.SelectAsync(x => x.Id == id);
            if (entity is null)
                throw new CustomException(404, "Product not found");

            entity.UpdatedAt = DateTime.UtcNow;
            entity.UpdatedBy = HttpContextHelper.UserId;
            var model = await productRepository.DeleteAsync(x => x == entity);

            return model;
        }

        public async ValueTask<ProductForResultDto> ModifyAsync(long id, ProductForUpdateDto model)
        {
            var entity = await productRepository.SelectAsync(x => x.Id == id);
            if (entity is null)
                throw new CustomException(404, "Product not found");

            var mapped = mapper.Map(model, entity);
            mapped.UpdatedAt = DateTime.UtcNow;
            mapped.UpdatedBy = HttpContextHelper.UserId;
            mapped.Update();

            await productRepository.UpdateAsync(mapped);
            return mapper.Map<ProductForResultDto>(mapped);
        }

        public async Task<IEnumerable<ProductForResultDto>> RetrieveAllAsync(PaginationParams @params)
        {
            var products = await productRepository.SelectAllAsync(p => !p.IsDeleted)
                .ToPagedList(@params)
                .ToListAsync();

            return mapper.Map<IEnumerable<ProductForResultDto>>(products);
        }

        public async ValueTask<ProductForResultDto> RetrieveAsync(long id)
        {
            var entity = await productRepository.SelectAsync(x => x.Id == id);
            return mapper.Map<ProductForResultDto>(entity);
        }
    }
}
