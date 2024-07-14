using SmartCity.Data;
using SmartCity.Models;
using SmartCity.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

using System.Diagnostics;
using System.Diagnostics.Metrics;

namespace SmartCity.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly SmartCityDbContext _context;

        public HomeController(ILogger<HomeController> logger, SmartCityDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        /// <summary>
        /// leituras para um dado dispositivo
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IActionResult Index()
        {
            var vm = new RelatorioVm { };

                vm.LastSet = MaisRecentes();
     

            return View(vm);
        }

        /// <summary>
        /// devolve leituras mais recentes
        /// </summary>
        /// <returns></returns>
        public MedicaoVm MaisRecentes()
        {
            var medicao_recente = new MedicaoVm();

            var last4 = _context.Medicoes.
                OrderByDescending(m => m.DataMedicao).Take(4).ToList();

            if (last4.Any())
            {
                var estacionamento1 = last4.FirstOrDefault(m => m.EstacionamentoId == 1);
                var estacionamento2 = last4.FirstOrDefault(m => m.EstacionamentoId == 2);
                var estacionamento3 = last4.FirstOrDefault(m => m.EstacionamentoId == 3);
                var estacionamento4 = last4.FirstOrDefault(m => m.EstacionamentoId == 4);
               

                if (estacionamento1 != null)
                {
                    medicao_recente.EstacionamentoId1 = estacionamento1.EstacionamentoId;
                    medicao_recente.DataMedicao = estacionamento1.DataMedicao;
                    medicao_recente.Estacionamento1 = estacionamento1.ValorLido;
                }
                if (estacionamento2 != null)
                {
                    medicao_recente.EstacionamentoId2 = estacionamento2.EstacionamentoId;
                    medicao_recente.DataMedicao = estacionamento2.DataMedicao;
                    medicao_recente.Estacionamento2 = estacionamento2.ValorLido;
                }
                if (estacionamento3 != null)
                {
                    medicao_recente.EstacionamentoId3 = estacionamento3.EstacionamentoId;
                    medicao_recente.DataMedicao = estacionamento3.DataMedicao;
                    medicao_recente.Estacionamento3 = estacionamento3.ValorLido;
                }
                if (estacionamento4 != null)
                {
                    medicao_recente.EstacionamentoId4 = estacionamento4.EstacionamentoId;
                    medicao_recente.DataMedicao = estacionamento4.DataMedicao;
                    medicao_recente.Estacionamento4 = estacionamento4.ValorLido;
                }
             


            }

            return medicao_recente;
        }


        /// <summary>
        /// devolve leituras
        /// </summary>
        /// <returns></returns>
        public IActionResult Todas()
        {
            List<Medicao> medicoes = new List<Medicao>();

            medicoes = _context.Medicoes.
                Include(t => t.Estacionamento).
                OrderByDescending(m => m.DataMedicao).ToList();

            return View(medicoes);
        }


        /// <summary>
        /// Método que recebe os valores lidos pelos sensores HC-SR04 e insere-os na base de dados
        /// <param name="estacionamento1"></param>
        /// /// <param name="estacionamento2"></param>
        /// /// <param name="estacionamento3"></param>
        /// /// <param name="estacionamento4"></param>
        /// <returns></returns>
        public ActionResult PostData(int? estacionamento1, int? estacionamento2, int? estacionamento3, int? estacionamento4)
        {
            var resultado = "Sucesso";
            var data_medicao = DateTime.Now;


            try
            {
                   

                    if (estacionamento1.HasValue)
                    {
                        // Adiciona Medição/Leitura do estacionamento 1
                        _context.Medicoes.Add(new Medicao
                        {
                            EstacionamentoId = (int)TipoMedicaoEnum.Estacionamento1,
                            ValorLido = estacionamento1.Value,
                            DataMedicao = data_medicao
                        });
                    }

                if (estacionamento2.HasValue)
                {
                    // Adiciona Medição/Leitura do estacionamento 2
                    _context.Medicoes.Add(new Medicao
                    {
                        EstacionamentoId = (int)TipoMedicaoEnum.Estacionamento2,
                        ValorLido = estacionamento2.Value,
                        DataMedicao = data_medicao
                    });
                }

                if (estacionamento3.HasValue)
                {
                    // Adiciona Medição/Leitura do estacionamento 3
                    _context.Medicoes.Add(new Medicao
                    {
                        EstacionamentoId = (int)TipoMedicaoEnum.Estacionamento3,
                        ValorLido = estacionamento3.Value,
                        DataMedicao = data_medicao
                    });
                }

                if (estacionamento4.HasValue)
                {
                    // Adiciona Medição/Leitura do estacionamento 4
                    _context.Medicoes.Add(new Medicao
                    {
                        EstacionamentoId = (int)TipoMedicaoEnum.Estacionamento4,
                        ValorLido = estacionamento4.Value,
                        DataMedicao = data_medicao
                    });
                }


                // gravar
                _context.SaveChanges();
                
            }
            catch (Exception ex)
            {
                resultado = "Erro: " + ex.Message;
            }

            return Content(resultado);
        }



        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
