using WebApiContaBancaria.Models.ContaBancariaModel;
using WebApiContaBancaria.Request.ContaBancaria;

namespace WebApiContaBancaria.Converters.ContaBancaria {
    public class ContaCreateRequestToContaModel {

        public ContaBancariaModel Convert(ContaBancariaCreateRequest contaBancariaCreateRequest) {
            return new ContaBancariaModel(
                contaBancariaCreateRequest.Nome,
                contaBancariaCreateRequest.Cnpj,
                contaBancariaCreateRequest.NumeroConta,
                contaBancariaCreateRequest.Agencia,
                contaBancariaCreateRequest.Banco,
                SalvarImagemConta(contaBancariaCreateRequest.ImageBase64)
            );
        }

        private string SalvarImagemConta(string imageBase64) {

            var filePath = "D:\\Development\\ImagensConta";

            var fileExt = imageBase64.Substring(imageBase64.IndexOf("/") + 1,
                          imageBase64.IndexOf(";") - imageBase64.IndexOf("/") - 1);

            var base64Code = imageBase64.Substring(imageBase64.IndexOf(",") + 1);

            var imgByte = System.Convert.FromBase64String(base64Code);

            var fileName = Guid.NewGuid().ToString() + "." + fileExt;

            using (var imageFile = new FileStream(filePath + "/" + fileName, FileMode.Create)) {
                imageFile.Write(imgByte, 0, imgByte.Length);
                imageFile.Flush();
            }

            return filePath + "/" + fileName;

        }
    }
}
