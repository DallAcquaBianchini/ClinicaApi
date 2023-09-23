﻿using ClinicaAPI.DataContext;
using ClinicaAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace ClinicaAPI.Service.FormacaoService
{
    public class FormacaoService : IFormacaoInterface
    {
        private readonly ApplicationDbContext _context;
        public FormacaoService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<ServiceResponse<List<FormacaoModel>>> CreateFormacao(FormacaoModel novoFormacao)
        {
            ServiceResponse<List<FormacaoModel>> serviceResponse = new ServiceResponse<List<FormacaoModel>>();

            try
            {
                if (novoFormacao == null)
                {
                    serviceResponse.Dados = null;
                    serviceResponse.Mensagem = "Informar dados...";
                    serviceResponse.Sucesso = false;
                    return serviceResponse;
                }
                _context.Add(novoFormacao);
                await _context.SaveChangesAsync();
                serviceResponse.Dados = _context.Formacaos.ToList();
                if (serviceResponse.Dados.Count == 0)
                {
                    serviceResponse.Mensagem = "Nenhum dado encontrado.";

                }
            }
            catch (Exception ex)
            {
                serviceResponse.Mensagem = ex.Message;
                serviceResponse.Sucesso = false;
            }
            return serviceResponse;
        }

        public async Task<ServiceResponse<List<FormacaoModel>>> GetFormacao()
        {
            ServiceResponse<List<FormacaoModel>> serviceResponse = new ServiceResponse<List<FormacaoModel>>();

            try
            {
                serviceResponse.Dados = _context.Formacaos.ToList();
                if (serviceResponse.Dados.Count == 0)
                {
                    serviceResponse.Mensagem = "Nenhum dado encontrado.";

                }
            }
            catch (Exception ex)
            {
                serviceResponse.Mensagem = ex.Message;
                serviceResponse.Sucesso = false;
            }
            return serviceResponse;
        }

        public async Task<ServiceResponse<List<FormacaoModel>>> UpdateFormacao(FormacaoModel editFormacao)
        {
            ServiceResponse<List<FormacaoModel>> serviceResponse = new ServiceResponse<List<FormacaoModel>>();
            try
            {
                FormacaoModel Formacao = _context.Formacaos.AsNoTracking().FirstOrDefault(x => x.Id == editFormacao.Id);


                if (Formacao == null)
                {
                    serviceResponse.Mensagem = "Nenhum dado encontrado.";
                    serviceResponse.Dados = null;
                    serviceResponse.Sucesso = false;
                }


                _context.Formacaos.Update(editFormacao);
                await _context.SaveChangesAsync();
                serviceResponse.Dados = _context.Formacaos.ToList();
            }
            catch (Exception ex)
            {
                serviceResponse.Mensagem = ex.Message;
                serviceResponse.Sucesso = false;
            }
            return serviceResponse;
        }

        public async Task<ServiceResponse<List<FormacaoModel>>> GetFormacaobyArea(string Area)
        {
            ServiceResponse<List<FormacaoModel>> serviceResponse = new ServiceResponse<List<FormacaoModel>>();

            try
            {
                List<FormacaoModel> Formacaos = _context.Formacaos
                    .Where(x => x.AreasRelacionadas.ToLower().Contains(Area.ToLower()))
                    .ToList();

                if (Formacaos.Count == 0)
                {
                    serviceResponse.Mensagem = "Nenhum dado encontrado.";
                    serviceResponse.Dados = null;
                    serviceResponse.Sucesso = false;
                }
                else
                {
                    serviceResponse.Dados = Formacaos;
                    serviceResponse.Sucesso = true;
                }
            }
            catch (Exception ex)
            {
                serviceResponse.Mensagem = ex.Message;
                serviceResponse.Sucesso = false;
            }
            return serviceResponse;
        }


        public async Task<ServiceResponse<FormacaoModel>> GetFormacaobyId(int Id)
        {
            ServiceResponse<FormacaoModel> serviceResponse = new ServiceResponse<FormacaoModel>();

            try
            {
                FormacaoModel Formacao = _context.Formacaos.FirstOrDefault(x => x.IdFuncionario == Id);


                if (Formacao == null)
                {
                    serviceResponse.Mensagem = "Nenhum dado encontrado.";
                    serviceResponse.Dados = null;
                    serviceResponse.Sucesso = false;
                }

                serviceResponse.Dados = Formacao;
            }
            catch (Exception ex)
            {
                serviceResponse.Mensagem = ex.Message;
                serviceResponse.Sucesso = false;
            }
            return serviceResponse;
        }

    }
}
