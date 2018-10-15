using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.ML.Legacy;
using Microsoft.ML.Legacy.Data;
using Microsoft.ML.Legacy.Models;
using Microsoft.ML.Legacy.Trainers;
using Microsoft.ML.Legacy.Transforms;

namespace ML
{
    class Program
    {
        static readonly string _datapath = Path.Combine(Environment.CurrentDirectory, "Data", "exchange.csv");
        static readonly string _testdatapath = Path.Combine(Environment.CurrentDirectory, "Data", "exchange.csv");
        static readonly string _modelpath = Path.Combine(Environment.CurrentDirectory, "Data", "Model.zip");

        static async Task Main(string[] args)
        {
            PredictionModel<ExchangeRate, ExchangeRatePrediction> model = await Train();
            ExchangeRatePrediction prediction = model.Predict(getTrip());
            Console.WriteLine("Predicted USD To CAD: {0}, Actual USD TO CAD: 1.0514", prediction.USDtoCAD);
            Console.ReadLine();
        }
        private static void Evaluate(PredictionModel<ExchangeRate, ExchangeRatePrediction> model)
        {
            var testData = new TextLoader(_testdatapath).CreateFrom<ExchangeRate>(useHeader: true, separator: ',');
            var evaluator = new RegressionEvaluator();
            RegressionMetrics metrics = evaluator.Evaluate(model, testData);

            Console.WriteLine($"Rms = {metrics.Rms}");
            Console.WriteLine($"RSquared = {metrics.RSquared}");
        }


        public static async Task<PredictionModel<ExchangeRate, ExchangeRatePrediction>> Train()
        {
            var pipeline = new LearningPipeline();

            pipeline.Add(new TextLoader(_datapath).CreateFrom<ExchangeRate>(useHeader: true, separator: ','));

            pipeline.Add(new ColumnCopier(("USDToCAD", "Label")));

            pipeline.Add(new CategoricalOneHotVectorizer("USDToEUR",
                "USDToJPY",
                "USDToCZK",
                "USDToGBP",
                "USDToAUD",
                "USDToBRL",
                "USDToCNY",
                "USDToZAR",
                "USDToMXN",
                "USDToARS",
                "USDToCHF",
                "USDToINR",
                "USDToVND",
                "USDToZMW",
                "USDToIDR",
                "USDToIQD",
                "USDToIRR",
                "DateYear",
                "DateMonth",
                "DayofMonth",
                "DayOfWeek"));

            pipeline.Add(new ColumnConcatenator("Features",
                "USDToEUR",
                "USDToJPY",
                "USDToCZK",
                "USDToGBP",
                "USDToAUD",
                "USDToBRL",
                "USDToCNY",
                "USDToZAR",
                "USDToMXN",
                "USDToARS",
                "USDToCHF",
                "USDToINR",
                "USDToVND",
                "USDToZMW",
                "USDToIDR",
                "USDToIQD",
                "USDToIRR",
                "DateYear",
                "DateMonth",
                "DayofMonth",
                "DayOfWeek"));

            pipeline.Add(new FastTreeRegressor());

            PredictionModel<ExchangeRate, ExchangeRatePrediction> model = pipeline.Train<ExchangeRate, ExchangeRatePrediction>();

            await model.WriteAsync(_modelpath);
            return model;

        }

        private static ExchangeRate getTrip()
        {
            return new ExchangeRate()
            {
                USDToEUR = 0.771,
                USDToJPY = 99.89,
                USDToCZK = 20.0563,
                USDToGBP = 0.6586,
                USDToAUD = 1.0959,
                USDToBRL = 2.2643,
                USDToCNY = 6.1796,
                USDToZAR = 10.0594,
                USDToMXN = 12.9264,
                USDToARS = 5.3958,
                USDToCHF = 0.9509,
                USDToINR = 60.1461,
                USDToVND = 21208.95,
                USDToZMW = 5.4692,
                USDToIDR = 9940.445,
                USDToIQD = 1163.29,
                USDToIRR = 12248.3,
                DateYear = 2013,
                DateMonth = 7,
                DayofMonth = 5,
                DayOfWeek = 5,
                USDToCAD = 0 // predict it. actual = 29.5
            };
        }

        public static async Task<ExchangeRatePrediction> Predict(ExchangeRate trip)
        {
            PredictionModel<ExchangeRate, ExchangeRatePrediction> _model = await PredictionModel.ReadAsync<ExchangeRate, ExchangeRatePrediction>(_modelpath);
            var prediction = _model.Predict(trip);
            return prediction;
        }

    }



}
