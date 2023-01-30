using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Empresa_Atividade
{
    static class NEmpresa
    {
        //private Turma[] turmas = new Turma[10];
        private static List<Empresa> empresas = new List<Empresa>();
        public static void Inserir(Empresa e)
        {
            Abrir();
            // Procurar o maior Id
            int id = 0;
            foreach (Empresa obj in empresas)
                if (obj.id > id) id = obj.id;
            e.id = id + 1;
            empresas.Add(e);
            Salvar();
        }
        public static List<Empresa> Listar()
        {
            Abrir();
            return empresas;
        }
        public static void Atualizar(Empresa e)
        {
            Abrir();
            // Percorrer a lista de turma procurando o id informado (e.Id)
            foreach (Empresa obj in empresas)
                if (obj.id == e.id)
                {
                    obj.nome = e.nome;
                    obj.ramo = e.ramo;
                    obj.endereco = e.endereco;
                }
            Salvar();
        }
        public static void Excluir(Empresa e)
        {
            Abrir();
            // Percorrer a lista de turma procurando o id informado (t.Id)
            Empresa x = null;
            foreach (Empresa obj in empresas)
                if (obj.id == e.id) x = obj;
            if (x != null) empresas.Remove(x);
            Salvar();
        }
        public static void Abrir()
        {
            StreamReader f = null;
            try
            {
                // Objeto que serializa (transforma) uma lista de turmas em um texto em XML
                XmlSerializer xml = new XmlSerializer(typeof(List<Empresa>));
                // Objeto que abre um texto em um arquivo
                f = new StreamReader("./turmas.xml");
                // Chama a operação de desserialização informando a origem do texto XML
                empresas = (List<Empresa>)xml.Deserialize(f);
            }
            catch
            {
                empresas = new List<Empresa>();
            }
            // Fecha o arquivo
            if (f != null) f.Close();
        }
        public static void Salvar()
        {
            // Objeto que serializa (transforma) uma lista de turmas em um texto em XML
            XmlSerializer xml = new XmlSerializer(typeof(List<Empresa>));
            // Objeto que grava um texto em um arquivo
            StreamWriter f = new StreamWriter("./turmas.xml", false);
            // Chama a operação de serialização informando o destino do texto XML
            xml.Serialize(f, empresas);
            // Fecha o arquivo
            f.Close();
        }
    }
}
