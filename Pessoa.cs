using System;
using System.Globalization;
using System.Runtime.InteropServices;

namespace teste
{
    class Pessoa
    {
        public string Name { get; set; }
        public int Id { get; private set; }
        public Double Salary { get; private set; }

        private static int _newId = 1;
        public Pessoa(string name, double salary)
        {
            Name = name;
            Id = _newId++;
            AlterSalary(salary);
        }

        public void AlterSalary(double newsalary)
        {
            if(newsalary > 0){
                Salary = newsalary;
            }else{
                Console.WriteLine("Salario Invalido! Deve ser maior que zero");
            }
            
        }

        public void AlterName(string newname)
        {
            Name = newname;
        }

        public override string ToString()
        {
            return $"Id: {Id} Nome: {Name} Salário: {Salary:C}";
        }
    }
}