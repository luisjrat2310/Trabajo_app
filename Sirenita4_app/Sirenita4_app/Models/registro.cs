using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace Sirenita4_app.Models
{
    public class registro
    {
        [PrimaryKey]
        public int Id { get; set; }
        public string nombre { get; set; }
        public string Apellidos { get; set;}
        public string correo { get; set; }
        public int telefono { get; set; }
        public int edad {  get; set; }
       

        public override string ToString()
        {
            return this.nombre + "\n" + this.Apellidos + "\n" + this.correo + "\n" + this.telefono + "\n" + this.edad;
        }
    }
}
