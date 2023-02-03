using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Empresa_Atividade
{
    static class NFuncionario
    {
        private static List<Funcionario> funcionarios = new List<Funcionario>();
        public static void Inserir(Funcionario f)
        { // C - Create
            Abrir();
            funcionarios.Add(f);
            Salvar();
        }
        public static List<Funcionario> Listar()
        { // R - Read
            Abrir();
            return funcionarios;
        }
        public static Funcionario Listar(int id)
        {
            // Encontra, se existir, uma turma com o id
            foreach (Funcionario obj in funcionarios)
                if (obj.id == id) return obj;
            return null;
        }
        public static void Atualizar(Funcionario f)
        { // U - Update
            Abrir();
            Funcionario obj = Listar(f.id);
            obj.nome = f.nome;
            obj.idade = f.idade;
            obj.email = f.email;
            obj.salario = f.salario;
            obj.cargo = f.cargo;
            obj.idSetor = f.idSetor;
            Salvar();
        }
        public static void Excluir(Funcionario f)
        { // D - Delete
            Abrir();
            funcionarios.Remove(Listar(f.id));
            Salvar();
        }
        public static void Abrir()
        {
            StreamReader f = null;
            try
            {
                XmlSerializer xml = new XmlSerializer(typeof(List<Funcionario>));
                f = new StreamReader("./Funcionario.xml");
                funcionarios = (List<Funcionario>)xml.Deserialize(f);
            }
            catch
            {
                funcionarios = new List<Funcionario>();
            }
            if (f != null) f.Close();
        }
        public static void Salvar()
        {
            XmlSerializer xml = new XmlSerializer(typeof(List<Funcionario>));
            StreamWriter f = new StreamWriter("./Funcionario.xml", false);
            xml.Serialize(f, funcionarios);
            f.Close();
        }
        public static void Matricular(Funcionario f, Setor s)
        {
            f.idSetor = s.id;
            Atualizar(f);
        }
        public static List<Funcionario> Listar(Setor s)
        {
            Abrir(); // Abre a lista com todos os funcionarios
            List<Funcionario> funci_setor = new List<Funcionario>(); // Lista de funcionarios da turma t
            foreach (Funcionario obj in funcionarios)
                if (obj.idSetor == s.id) funci_setor.Add(obj);
            return funci_setor;
        }
    }
}
