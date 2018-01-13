using BuscadorDeCompatibilidadeWeb.Models;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web.Mvc;

namespace BuscadorDeCompatibilidadeWeb.Controllers
{
    public class HomeController : Controller
    {
        #region Constantes

        public const string PASTA_APP_DATA = "~/App_Data/";
        public const string NOME_ARQUIVO_EXCEL_VAGAS = "Vagas-report.xlsx";
        public const string NOME_ARQUIVO_EXCEL_VOLUNTARIOS = "Cadastro Estudante da Pátria-report.xlsx";
        public const string CONHECIMENTO_1 = "Word";
        public const string CONHECIMENTO_2 = "Excel";
        public const string CONHECIMENTO_3 = "PowerPoint";
        public const string CONHECIMENTO_4 = "Project";
        public const string CONHECIMENTO_5 = "Customer Relationship Management (CRM)";
        public const string CONHECIMENTO_6 = "Photoshop";
        public const string CONHECIMENTO_7 = "Corel";
        public const string CONHECIMENTO_8 = "Illustrator";
        public const string CONHECIMENTO_9 = "Fotografia";
        public const string CONHECIMENTO_10 = "InDesign";
        public const string NIVEL_0 = "Não sabe";
        public const string NIVEL_1 = "Sabe com ajuda";
        public const string NIVEL_2 = "Sabe com autonomia";
        public const string NIVEL_3 = "Sabe ensinar";

        #endregion

        #region Métodos

        public object LerPlanilha(string planilha)
        {
            using (var fileStream = new FileStream(Server.MapPath(PASTA_APP_DATA + planilha), FileMode.Open, FileAccess.Read))
            {
                using (var ep = new ExcelPackage(fileStream))
                {
                    var ws = ep.Workbook.Worksheets["results"];

                    //Ignora o cabeçalho
                    int firstContentRow = ws.Dimension.Start.Row + 1;
                    int lastContentRow = ws.Dimension.End.Row;
                    int firstContentColumn = ws.Dimension.Start.Column;
                    int lastContentColumn = ws.Dimension.End.Column;

                    List<object> itens = new List<object>();

                    for (int i = firstContentRow; i <= lastContentRow; i++)
                    {
                        var item = new List<string>();

                        for (int j = firstContentColumn; j <= lastContentColumn; j++)
                        {
                            item.Add(ws.Cells[i, j].GetValue<string>());
                        }

                        itens.Add(item);
                    }

                    if (planilha.Equals(NOME_ARQUIVO_EXCEL_VAGAS))
                    {
                        return PreencherVagaModel(itens);
                    }
                    else
                    {
                        return PreencherVoluntarioModel(itens);
                    }
                }
            }

        }

        public object PreencherVagaModel(List<object> itens)
        {
            VagaModel model = new VagaModel();
            model.ListaVaga = new List<Vaga>();

            for (int i = 0; i < itens.Count; i++)
            {
                List<string> item = itens[i] as List<string>;
                Vaga objVaga = new Vaga();
                objVaga.VagaID = item[0];
                objVaga.VagaNome = item[1];
                objVaga.Word = item[2];
                objVaga.Excel = item[3];
                objVaga.PowerPoint = item[4];
                objVaga.Project = item[5];
                objVaga.CRM = item[6];
                objVaga.Photoshop = item[7];
                objVaga.Corel = item[8];
                objVaga.Illustrator = item[9];
                objVaga.Fotografia = item[10];
                objVaga.InDesign = item[11];

                model.ListaVaga.Add(objVaga);
            }

            return model;
        }

        public object PreencherVoluntarioModel(List<object> itens)
        {
            List<VoluntarioModel> voluntarios = new List<VoluntarioModel>();

            for (int i = 0; i < itens.Count; i++)
            {
                List<string> item = itens[i] as List<string>;
                VoluntarioModel voluntario = new VoluntarioModel();
                voluntario.NomeCompleto = item[1];
                voluntario.Genero = item[2];
                voluntario.RG = item[3];
                voluntario.CPF = item[4];
                voluntario.EnderecoCompleto = item[5];
                voluntario.Bairro = item[6];
                voluntario.CEP = item[7];
                voluntario.Cidade = item[8];
                voluntario.Estado = item[9];
                voluntario.TelefoneResidencial = item[10];
                voluntario.TelefoneCelular = item[11];
                voluntario.Email = item[12];
                voluntario.NivelDeEscolaridade = item[13];
                voluntario.Profissao = item[14];
                voluntario.AreaEmQueDesejaAtuar = item[15];
                voluntario.Other = item[16];
                voluntario.PossuiExperienciaEmProjetosSociais = item[17];
                voluntario.Quais = item[18];
                voluntario.De = item[19];
                voluntario.Ate = item[20];
                voluntario.DisponibilidadeDeDias = item[21];
                voluntario.Manha = item[22];
                voluntario.Tarde = item[23];
                voluntario.Word = item[24];
                voluntario.Excel = item[25];
                voluntario.PowerPoint = item[26];
                voluntario.Project = item[27];
                voluntario.CRM = item[28];
                voluntario.Photoshop = item[29];
                voluntario.Corel = item[30];
                voluntario.Illustrator = item[31];
                voluntario.Fotografia = item[32];
                voluntario.InDesign = item[33];

                voluntarios.Add(voluntario);
            }

            return voluntarios;
        }

        public List<string> VerificarConhecimentosVaga(Vaga vagaSelecionada)
        {
            List<string> conhecimentosVaga = new List<string>();
            var props = vagaSelecionada.GetType().GetProperties();

            foreach (var prop in props)
            {
                string nomeConhecimentoVaga = prop.GetValue(vagaSelecionada).ToString();
                if (nomeConhecimentoVaga != "" && prop.Name != "VagaID" && prop.Name != "VagaNome")
                {
                    conhecimentosVaga.Add(nomeConhecimentoVaga);
                }
            }

            return conhecimentosVaga;
        }

        public static short ConverterParaPontuacao(string NivelConhecimento)
        {
            switch (NivelConhecimento)
            {
                case NIVEL_1: return 1;
                case NIVEL_2: return 2;
                case NIVEL_3: return 3;
                case NIVEL_0:
                default:
                    return 0;
            }
        }

        #endregion

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Upload()
        {
            if (Request.Files.Count > 0)
            {
                for (int i = 0; i < Request.Files.Count; i++)
                {
                    var file = Request.Files[i];

                    if (file != null && file.ContentLength > 0)
                    {
                        var fileName = Path.GetFileName(file.FileName);
                        var path = Path.Combine(Server.MapPath(PASTA_APP_DATA), fileName);
                        file.SaveAs(path);
                    }
                }
            }

            return RedirectToAction("Vagas");
        }

        public ActionResult Vagas()
        {
            VagaModel model = LerPlanilha(NOME_ARQUIVO_EXCEL_VAGAS) as VagaModel;
            
            TempData["VagaModel"] = model;

            return View(model);
        }

        [HttpPost]
        public ActionResult SelecionarVaga(string ID)
        {
            VagaModel model = TempData["VagaModel"] as VagaModel;

            var vagaSelecionada = model.ListaVaga.First(m => m.VagaID.Equals(ID)) as Vaga;
            var conhecimentosVaga = VerificarConhecimentosVaga(vagaSelecionada);
            int quantidadeConhecimentosVaga = conhecimentosVaga.Count();
            int pontuacaoVaga = quantidadeConhecimentosVaga * 3;
            
            List<VoluntarioModel> voluntarios = LerPlanilha(NOME_ARQUIVO_EXCEL_VOLUNTARIOS) as List<VoluntarioModel>;
            
            int quantidadeVoluntarios = voluntarios.Count();

            for (int i = 0; i < quantidadeVoluntarios; i++)
            {
                int pontuacaoCandidato = 0;
                for (int j = 0; j < quantidadeConhecimentosVaga; j++)
                {
                    switch (conhecimentosVaga[j])
                    {
                        case CONHECIMENTO_1:
                            pontuacaoCandidato += ConverterParaPontuacao(voluntarios[i].Word);
                            break;
                        case CONHECIMENTO_2:
                            pontuacaoCandidato += ConverterParaPontuacao(voluntarios[i].Excel);
                            break;
                        case CONHECIMENTO_3:
                            pontuacaoCandidato += ConverterParaPontuacao(voluntarios[i].PowerPoint);
                            break;
                        case CONHECIMENTO_4:
                            pontuacaoCandidato += ConverterParaPontuacao(voluntarios[i].Project);
                            break;
                        case CONHECIMENTO_5:
                            pontuacaoCandidato += ConverterParaPontuacao(voluntarios[i].CRM);
                            break;
                        case CONHECIMENTO_6:
                            pontuacaoCandidato += ConverterParaPontuacao(voluntarios[i].Photoshop);
                            break;
                        case CONHECIMENTO_7:
                            pontuacaoCandidato += ConverterParaPontuacao(voluntarios[i].Corel);
                            break;
                        case CONHECIMENTO_8:
                            pontuacaoCandidato += ConverterParaPontuacao(voluntarios[i].Illustrator);
                            break;
                        case CONHECIMENTO_9:
                            pontuacaoCandidato += ConverterParaPontuacao(voluntarios[i].Fotografia);
                            break;
                        case CONHECIMENTO_10:
                        default:
                            pontuacaoCandidato += ConverterParaPontuacao(voluntarios[i].InDesign);
                            break;
                    }
                }

                //Cálculo de compatibilidade voluntário x vaga
                voluntarios[i].Compatibilidade = string.Concat(pontuacaoCandidato * 100 / pontuacaoVaga, "%");

                //Formatações
                if (voluntarios[i].Other != string.Empty)
                {
                    voluntarios[i].AreaEmQueDesejaAtuar = voluntarios[i].Other;
                }

                voluntarios[i].PossuiExperienciaEmProjetosSociais =
                    voluntarios[i].PossuiExperienciaEmProjetosSociais.Equals("1") ? "Sim" : "Não";

            }

            //Ordena por compatiblidade
            var voluntariosOrdenados = voluntarios.OrderByDescending(x => x.Compatibilidade).ToList();

            TempData["voluntariosOrdenados"] = voluntariosOrdenados;

            return RedirectToAction("Voluntarios");
        }

        public ActionResult Voluntarios()
        {
            List<VoluntarioModel> model = (List<VoluntarioModel>)TempData["voluntariosOrdenados"];

            return View(model);
        }

    }
}