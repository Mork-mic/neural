using neural;


Neuron[][] neurons =

{

    new Neuron[2],

    new Neuron[2],

    new Neuron[1]

};

int set = 1;

neurons = Helper.GenerateNeurons(neurons);

neurons[0][0].Value = 1;

neurons[0][0].Connections[0].Weight = 0.45;

neurons[0][0].Connections[1].Weight = 0.78;

neurons[0][1].Connections[0].Weight = -0.12;



neurons[0][1].Value = 0;

neurons[0][0].Name = "I1";

neurons[0][1].Name = "I2";

neurons[1][0].Name = "H1";

neurons[1][1].Name = "H2";

neurons[2][0].Name = "O1";


neurons[0][1].Connections[0].Weight = -0.12; //w3

neurons[0][1].Connections[1].Weight = 0.13; //w4

neurons[1][0].Connections[0].Weight = 1.5;//w5

neurons[1][1].Connections[0].Weight = -2.3; // w6


Helper.StartActivation(neurons);

double error = (double.Pow(1 - neurons[2][0].Value, 2)) / set;

Console.WriteLine(neurons[2][0].Value);


var w5 = Helper.DeltaW(1, neurons[2][0], neurons[1][0]);

Console.WriteLine("W5 - " + w5); // W5 ! +

neurons[1][0].Connections[0].Weight += w5;


var w6 = Helper.DeltaW(1, neurons[2][0], neurons[1][1]);

Console.WriteLine("W6 - " + w6); // W6 ! +

neurons[1][1].Connections[0].Weight += w6;


var w4 = Helper.DeltaW(1, neurons[1][1], neurons[0][1]);

Console.WriteLine("W4 - " + w4); // W4 !

neurons[0][1].Connections[1].Weight += w4;