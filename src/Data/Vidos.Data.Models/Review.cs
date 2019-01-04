namespace Vidos.Data.Models
{
    public class Review : BaseModel
    {
        /*Value from 1(worst) to 5(best)*/
        public int Rating { get; set; }

        public string Body { get; set; }

        public string ProductId { get; set; }

        public AirConditioner Product { get; set; }

        public string ClientId { get; set; }

        public VidosUser Client { get; set; }
    }
}
