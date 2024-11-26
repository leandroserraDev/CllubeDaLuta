namespace EncontroDeLutadores.API.Response
{
    public class CustomResponse
    {
        public CustomResponse(bool success, object data, IList<string>? errors = null)
        {
            this.success = success;
           this.data = data;
            this. errors = errors;
        }

        public bool success{ get; private set; }
        public object data { get; private set; }
        public IList<string>? errors { get; private set; } = null;
    }
}
