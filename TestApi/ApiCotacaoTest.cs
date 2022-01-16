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
    public class ApiCotacaoTest
    {
        CotacaoController _controller;
        Cotacao insertObj = new Cotacao
        {
            Cnpjcomprador = "77.777.777/7777-77",
            Cnpjfornecedor = "88.888.888/8888-88",
            NumeroCotacao = 1,
            DataCotacao = DateTime.Now,
            DataEntregaCotacao = DateTime.Now.AddDays(7),
            Cep = "40283-170",
            Logradouro = string.Empty,
            Complemento = string.Empty,
            Bairro = string.Empty,
            Uf = string.Empty,
            Observacao = string.Empty,
        };

        public ApiCotacaoTest()
        {
            _controller = new CotacaoController(new CotacaoRepository(new ApiContext()));
        }

        [Fact]
        public async void GetAllCotacao()
        {
            var result = await _controller.GetCotacoes();
            Assert.IsType<OkObjectResult>(result.Result);
        }

        [Fact]
        public async void GetCotacao()
        {
            var result = await _controller.GetCotacao(1);
            Assert.IsType<OkObjectResult>(result.Result);
        }

        [Fact]
        public async void InsertCotacao()
        {
            var result = await _controller.InsertCotacao(insertObj);
            Assert.True(((Cotacao)((ObjectResult)result).Value).Id > 0);
        }

        [Fact]
        public async void UpdateCotacao()
        {
            int id = 1;
            var cotacaoResult = await _controller.GetCotacao(id);
            Cotacao cotacao = ((Cotacao)((ObjectResult)cotacaoResult.Result).Value);

            //Random random = new Random();
            //const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            //string obervacao = new string(Enumerable.Repeat(chars, 10).Select(s => s[random.Next(s.Length)]).ToArray());
            
            //cotacao.Observacao = obervacao;
            //Assert.True(((Cotacao)((ObjectResult)result).Value).Observacao == obervacao);

            var result = await _controller.UpdateCotacao(id, cotacao); 
            Assert.True(((string)((ObjectResult)result).Value) == "Atualização da Cotação realizada com sucesso");
        }

        [Fact]
        public async void DeleteCotacao()
        {
            var resultInsert = await _controller.InsertCotacao(insertObj);
            int id = ((Cotacao)((ObjectResult)resultInsert).Value).Id;
            //Assert.True(id > 0);

            var resultDelete = await _controller.DeleteCotacao(id);
            Assert.IsType<OkObjectResult>(resultDelete.Result);
        }
    }
}
