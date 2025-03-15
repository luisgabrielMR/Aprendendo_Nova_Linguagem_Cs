using System;
using System.Globalization;
using System.Linq;
using System.Collections.Generic;

namespace teste
{
    class Program
    {
        private static void Main(string[] args)
        {
            List<Pessoa> ListPerson = new List<Pessoa>();
            Console.Clear();

            while (true)
            {
                int option;
                ShowMenu();

                if (int.TryParse(Console.ReadLine(), out option))
                {
                    switch (option)
                    {
                        case 1: // Cadastro
                            Cadastro(ListPerson);
                            break;

                        case 2: // Mostrar Cadastros
                            MostrarCadastros(ListPerson);
                            break;

                        case 3: // Editar Cadastros
                            EditarCadastro(ListPerson);
                            break;

                        case 4: // Excluir
                            ExcluirCadastro(ListPerson);
                            break;

                        case 5: // Sair
                            Console.Clear();
                            Console.WriteLine("Encerrando...");
                            return;

                        default:
                            Console.WriteLine("Opção inválida!");
                            break;
                    }
                }
                else
                {
                    Console.WriteLine("Opção inválida!");
                }
            }
        }

        private static void ShowMenu()
        {
            Console.WriteLine("Escolha uma opção:");
            Console.WriteLine("1 - Cadastrar");
            Console.WriteLine("2 - Mostrar Cadastros");
            Console.WriteLine("3 - Editar Cadastros");
            Console.WriteLine("4 - Excluir");
            Console.WriteLine("5 - Sair");
            Console.Write("Digite sua opção: ");
        }

        private static void Cadastro(List<Pessoa> ListPerson)
        {
            Console.Clear();
            double Salary;
            string Name;

            Name = GetValidName();
            Salary = GetValidSalary();

            ListPerson.Add(new Pessoa(Name, Salary));
            Console.Clear();
            Console.WriteLine("Cadastro realizado com sucesso!");
        }

        private static string GetValidName()
        {
            string name;
            while (true)
            {
                Console.WriteLine("Digite o Nome:");
                name = Console.ReadLine()?.Trim() ?? "";
                if (!string.IsNullOrWhiteSpace(name)) return name;
                Console.WriteLine("Nome inválido! Por favor, digite um nome válido.");
            }
        }

        private static double GetValidSalary()
        {
            double salary;
            while (true)
            {
                Console.WriteLine("Digite o Salário:");
                if (double.TryParse(Console.ReadLine(), NumberStyles.Any, CultureInfo.InvariantCulture, out salary) && salary > 0)
                    return salary;
                Console.WriteLine("Salário inválido! Deve ser um valor maior que zero.");
            }
        }

        private static void MostrarCadastros(List<Pessoa> ListPerson)
        {
            Console.Clear();
            if (ListPerson.Count == 0)
            {
                Console.WriteLine("Nenhum cadastro encontrado.");
            }
            else
            {
                foreach (Pessoa person in ListPerson)
                {
                    Console.WriteLine(person);
                }
            }
        }

        private static void EditarCadastro(List<Pessoa> ListPerson)
        {
            Console.Clear();
            if (ListPerson.Count == 0)
            {
                Console.WriteLine("Nenhum cadastro para editar.");
                return;
            }

            int optionId = GetValidId(ListPerson);
            var person = ListPerson.FirstOrDefault(p => p.Id == optionId);

            if (person != null)
            {
                Console.WriteLine("1 - Alterar o nome");
                Console.WriteLine("2 - Alterar o Salário");
                Console.WriteLine("3 - Alterar o Nome e o Salário");

                if (int.TryParse(Console.ReadLine(), out int optionAlter))
                {
                    AlterarCadastro(person, optionAlter);
                }
                else
                {
                    Console.WriteLine("Opção inválida!");
                }
            }
            else
            {
                Console.WriteLine("Id não encontrado!");
            }
        }

        private static void ExcluirCadastro(List<Pessoa> ListPerson)
        {
            Console.Clear();
            if (ListPerson.Count == 0)
            {
                Console.WriteLine("Nenhum cadastro para excluir.");
                return;
            }

            int idExclude = GetValidId(ListPerson);
            var person = ListPerson.FirstOrDefault(p => p.Id == idExclude);

            if (person != null)
            {
                ListPerson.Remove(person);
                Console.WriteLine("Cadastro excluído com sucesso!");
            }
            else
            {
                Console.WriteLine("Id não encontrado!");
            }
        }

        private static int GetValidId(List<Pessoa> ListPerson)
        {
            int optionId;
            foreach (Pessoa person in ListPerson)
            {
                Console.WriteLine(person);
            }

            Console.WriteLine("Digite o ID:");
            while (!int.TryParse(Console.ReadLine(), out optionId) || ListPerson.FirstOrDefault(p => p.Id == optionId) == null)
            {
                Console.WriteLine("Id inválido! Tente novamente.");
            }

            return optionId;
        }

        private static void AlterarCadastro(Pessoa person, int optionAlter)
        {
            switch (optionAlter)
            {
                case 1: // Alterar Nome
                    Console.WriteLine("Novo nome:");
                    string newName = Console.ReadLine()?.Trim() ?? "";
                    if (!string.IsNullOrWhiteSpace(newName))
                    {
                        person.AlterName(newName);
                        Console.WriteLine("Nome alterado com sucesso!");
                    }
                    else
                    {
                        Console.WriteLine("Nome inválido! Alteração cancelada.");
                        return; // Encerrando a alteração se o nome for inválido
                    }
                    break;

                case 2: // Alterar Salário
                    Console.WriteLine("Novo Salário:");
                    if (double.TryParse(Console.ReadLine(), NumberStyles.Any, CultureInfo.InvariantCulture, out double newSalary))
                    {
                        person.AlterSalary(newSalary);
                        Console.WriteLine("Salário alterado com sucesso!");
                    }
                    else
                    {
                        Console.WriteLine("Salário inválido!");
                    }
                    break;

                case 3: // Alterar Nome e Salário
                    Console.WriteLine("Novo nome:");
                    string newNames = Console.ReadLine()?.Trim() ?? "";
                    if (string.IsNullOrWhiteSpace(newNames))
                    {
                        Console.WriteLine("Nome inválido! Alteração cancelada.");
                        return; // Encerrando a alteração se o nome for inválido
                    }
                    person.AlterName(newNames);

                    Console.WriteLine("Novo Salário:");
                    if (double.TryParse(Console.ReadLine(), NumberStyles.Any, CultureInfo.InvariantCulture, out double newSalarys))
                        person.AlterSalary(newSalarys);
                    else
                        Console.WriteLine("Salário inválido!");

                    break;

                default:
                    Console.WriteLine("Opção inválida!");
                    break;
            }
        }

    }
}
