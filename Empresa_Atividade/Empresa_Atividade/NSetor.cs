
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Empresa_Atividade
{
    static class NSetor
    {
        //private Turma[] turmas = new Turma[10];
        private static List<Setor> setores = new List<Setor>();
        public static void Inserir(Setor s)
        {
            Abrir();
            // Procurar o maior Id
            int id = 0;
            foreach (Setor obj in setores)
                if (obj.id > id) id = obj.id;
            s.id = id + 1;
            setores.Add(s);
            Salvar();
        }
        public static List<Setor> Listar()
        {
            Abrir();
            return setores;
        }
        public static void Atualizar(Setor s)
        {
            Abrir();
            // Percorrer a lista de turma procurando o id informado (e.Id)
            foreach (Setor obj in setores)
                if (obj.id == s.id)
                {
                    obj.tipo = s.tipo;
                    obj.responsavel = s.responsavel;
                    obj.idEmpresa = s.idEmpresa;
                }
            Salvar();
        }
        public static void Excluir(Setor s)
        {
            Abrir();
            // Percorrer a lista de turma procurando o id informado (t.Id)
            Setor x = null;
            foreach (Setor obj in setores)
                if (obj.id == s.id) x = obj;
            if (x != null) setores.Remove(x);
            Salvar();
        }
        public static void Abrir()
        {
            StreamReader f = null;
            try
            {
                // Objeto que serializa (transforma) uma lista de turmas em um texto em XML
                XmlSerializer xml = new XmlSerializer(typeof(List<Setor>));
                // Objeto que abre um texto em um arquivo
                f = new StreamReader("./turmas.xml");
                // Chama a operação de desserialização informando a origem do texto XML
                setores = (List<Setor>)xml.Deserialize(f);
            }
            catch
            {
                setores = new List<Setor>();
            }
            // Fecha o arquivo
            if (f != null) f.Close();
        }
        public static void Salvar()
        {
            // Objeto que serializa (transforma) uma lista de turmas em um texto em XML
            XmlSerializer xml = new XmlSerializer(typeof(List<Setor>));
            // Objeto que grava um texto em um arquivo
            StreamWriter f = new StreamWriter("./turmas.xml", false);
            // Chama a operação de serialização informando o destino do texto XML
            xml.Serialize(f, setores);
            // Fecha o arquivo
            f.Close();
        }
        public static void Matricular(Setor s, Empresa e)
        {
            s.idEmpresa = e.id;
            Atualizar(s);
        }
        public static List<Setor> Listar(Empresa e)
        {
            Abrir(); // Abre a lista com todos os funcionarios
            List<Setor> setor_empresa = new List<Setor>(); // Lista de funcionarios da turma t
            foreach (Setor obj in setores)
                if (obj.idEmpresa == e.id) setor_empresa.Add(obj);
            return setor_empresa;
        }
    }
}