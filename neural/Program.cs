using neural;

Neuron[][] neurons =
{
    new Neuron[2],
    new Neuron[2],
    new Neuron[1]
};
double[][] vars = [[0, 0, 0], [0, 1, 1], [1, 0, 1], [1, 1, 0]];
int set = 1;
neurons = Helper.GenerateNeurons(neurons);
neurons[0][0].Value = 1;
neurons[0][0].Connections[0].Weight = 0.45;
neurons[0][0].Connections[1].Weight = 0.78;
neurons[0][1].Connections[0].Weight = -0.12;
neurons[0][1].Connections[1].Weight = 0.13;
neurons[1][0].Connections[0].Weight = 1.5;
neurons[1][1].Connections[0].Weight = -2.3;
neurons[0][1].Value = 0;

Helper.StartActivation(neurons);
double error = (double.Pow(vars[2][2] - neurons[2][0].Value, 2)) / set;
Console.WriteLine(neurons[2][0].Value);
Console.WriteLine(Helper.DeltaW(1, neurons[2][0].Value, neurons[1][0].Value));

/*Console.WriteLine(Helper.ActivateNeurons(neurons, neurons[1][0]).Value);
Console.WriteLine(Helper.ActivateNeurons(neurons, neurons[1][1]).Value);
Console.WriteLine(Helper.ActivateNeurons(neurons, neurons[2][0]).Value);*/