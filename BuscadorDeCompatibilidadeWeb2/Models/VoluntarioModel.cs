using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BuscadorDeCompatibilidadeWeb.Models
{
    public class VoluntarioModel : Conhecimento
    {
        public const string VAGA_DESEJADA = "Vaga desejada";
        public const string NOME_COMPLETO = "Nome completo";
        public const string GENERO = "Gênero";
        public const string DATA_NASCIMENTO = "Data de nascimento";
        public const string ENDERECO_COMPLETO = "Endereço completo";
        public const string TEL_RESIDENCIAL = "Telefone residencial";
        public const string CELULAR = "Telefone celular";
        public const string ESCOLARIDADE = "Nível de Escolaridade";
        public const string PROFISSAO = "Profissão";
        public const string AREA = "Área em que deseja atuar";
        public const string POSSUI_EXPERIENCIA = "Possui experiência em projetos sociais?";
        public const string QUAIS = "Quais?";
        public const string ATE = "até";
        public const string DISPONIBILIDADE = "Disponibilidade de dias";
        public const string MANHA = "Manhã";

        [Display(Name = VAGA_DESEJADA)]
        public string VagaDesejada { get; set; }
        [Display(Name = NOME_COMPLETO)]
        public string NomeCompleto { get; set; }
        [Display(Name = GENERO)]
        public string Genero { get; set; }
        [Display(Name = DATA_NASCIMENTO)]
        public string DataNascimento { get; set; }
        public string RG { get; set; }
        public string CPF { get; set; }
        [Display(Name = ENDERECO_COMPLETO)]
        public string EnderecoCompleto { get; set; }
        public string Bairro { get; set; }
        public string CEP { get; set; }
        public string Cidade { get; set; }
        public string Estado { get; set; }
        [Display(Name = TEL_RESIDENCIAL)]
        public string TelefoneResidencial { get; set; }
        [Display(Name = CELULAR)]
        public string TelefoneCelular { get; set; }
        public string Email { get; set; }
        [Display(Name = ESCOLARIDADE)]
        public string NivelDeEscolaridade { get; set; }
        [Display(Name = PROFISSAO)]
        public string Profissao { get; set; }
        [Display(Name = AREA)]
        public string AreaEmQueDesejaAtuar { get; set; }
        public string Other { get; set; }
        [Display(Name = POSSUI_EXPERIENCIA)]
        public string PossuiExperienciaEmProjetosSociais { get; set; }
        [Display(Name = QUAIS)]
        public string Quais { get; set; }
        public string De { get; set; }
        [Display(Name = ATE)]
        public string Ate { get; set; }
        [Display(Name = DISPONIBILIDADE)]
        public string DisponibilidadeDeDias { get; set; }
        [Display(Name = MANHA)]
        public string Manha { get; set; }
        public string Tarde { get; set; }
        public string Compatibilidade { get; set; }
    }
}