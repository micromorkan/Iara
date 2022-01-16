using Api.Context;
using Api.Controllers;
using Api.Models;
using Api.Repositories;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace TestApi
{
    public class ApiCotacaoItemTest
    {
        CotacaoItemController _controller;
        CotacaoItem insertObj = new CotacaoItem
        {
            IdCotacao = 1,
            Descricao = "Descrição",
            NumeroItem = 1,
            Preco = null,
            Quantidade = 1,
            Marca = string.Empty,
            Unidade = string.Empty,
        };

        public ApiCotacaoItemTest()
        {
            _controller = new CotacaoItemController(new CotacaoItemRepository(new ApiContext()));
        }

        [Fact]
        public async void InsertCotacaoItem()
        {
            var result = await _controller.InsertCotacaoItem(insertObj);
            Assert.True(((CotacaoItem)((ObjectResult)result).Value).Id > 0);
        }

        [Fact]
        public async void GetCotacaoItem()
        {
            var result = await _controller.GetCotacaoItem(2004);
            Assert.IsType<OkObjectResult>(result.Result);
        }        

        [Fact]
        public async void UpdateCotacaoItem()
        {
            int id = 2004;
            var cotacaoResult = await _controller.GetCotacaoItem(id);
            CotacaoItem cotacaoItem = ((CotacaoItem)((ObjectResult)cotacaoResult.Result).Value);

            var result = await _controller.UpdateCotacaoItem(id, cotacaoItem); 
            Assert.True(((string)((ObjectResult)result).Value) == "Atualização do Item da Cotação realizada com sucesso");
        }

        [Fact]
        public async void DeleteCotacaoItem()
        {
            var resultInsert = await _controller.InsertCotacaoItem(insertObj);
            int id = ((CotacaoItem)((ObjectResult)resultInsert).Value).Id;

            var resultDelete = await _controller.DeleteCotacaoItem(id);
            Assert.IsType<OkObjectResult>(resultDelete.Result);
        }
    }
}
