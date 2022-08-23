namespace HungryPizza.Models
{
    public class ApiReturnModel
    {
        public ApiReturnModel()
        {
            Model = null;
        }

        public ApiReturnModel(object model, IEnumerable<string> erroList = null)
        {
            ErroList = erroList != null ? erroList.Where(x => !string.IsNullOrEmpty(x)).ToList() : new List<string>();
            Model = model;
        }

        public ApiReturnModel(Exception exception)
        {
            ErroList = new List<string>() { exception.Message };
            Model = null;
        }

        public bool IsSuccess
        {
            get
            {
                if (ErroList.Any())
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
        }
        public IEnumerable<string> ErroList { get; set; } = new List<string>();
        public object Model { get; set; }
    }
}
