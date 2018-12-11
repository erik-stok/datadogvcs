using DataDog;

namespace ddvcs
{
    public class ValidateKeyUseCase
    {
        private readonly IDataDogGateway _dataDogGateway;
        private readonly IOutputWriter _outputWriter;

        public ValidateKeyUseCase(IOutputWriter outputWriter, IDataDogGateway dataDogGateway)
        {
            _outputWriter = outputWriter;
            _dataDogGateway = dataDogGateway;
        }

        public bool Execute()
        {
            var isValid = _dataDogGateway.ApiKeyIsValid();


            if (isValid)
            {
                _outputWriter.Write("API key is valid");
            }
            else
            {
                _outputWriter.Write("API key is NOT valid");
            }

            return isValid;
        }
    }
}
