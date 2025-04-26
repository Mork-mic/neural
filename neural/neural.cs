namespace neural;

public class Neuron
{
    public int Id { get; set; }
    
    public string Name { get; set; }
    public int Layer { get; set; }
    public int SynapsysConunt { get; set; }
    public double Value { get; set; }
    
    public List<Synapsys> Connections = new List<Synapsys>();
    
    
    
}
public class Synapsys
{
    public int Id { get; set; }
    
    public double Weight { get; set; }
    
    public Neuron InputNeuron { get; set; } // input
    
    public Neuron OutputNeuron { get; set; } // output
}

static class Helper
{
    static int id = 0;
    public static double LogSigmoid(double x)
    {
        if (x < -45.0) return 0.0;
        else if (x > 45.0) return 1.0;
        else return 1.0 / (1.0 + Math.Exp(-x));
    }

    public static double fIN(double OUT)
    {
        return (1 - OUT) * OUT;
    }

    public static double deltaO(double ideal, double actual)
    {
        return (ideal - actual) * fIN(actual);
    }

    /*public static double DeltaH(double ideal, double actual)
    {
        
    }

    public static double E(Neuron H)
    {
        
    }*/
    public static double Grad(double delta, double value)
    {
        return value * delta;
    }

    public static double DeltaW(double ideal, double actual, double value)
    {
        return 0.7 * Grad(deltaO(ideal, actual), value) + 0.3 * 0;
    }
    public static double deltaH(Neuron outPutN, Neuron inPutN, double ideal)

    {

        double sum = 0;

        foreach (var sin in outPutN.Connections)

        {

            sum += sin.Weight * deltaO(ideal, sin.OutputNeuron.Value);

        }

        var x = fIN(outPutN.Value) * sum;

        return x;

    }
    public static double DeltaW(double ideal, Neuron outPutN, Neuron inPutN)

    {

        if (outPutN.Connections.Count > 0)

        {

            var delta = deltaH(outPutN, inPutN, ideal);

            var grad = Grad(delta, inPutN.Value);

            return 0.7 * grad + 0.3 * 0;

        }

        else

        {

            var delta = deltaO(ideal, outPutN.Value);

            return 0.7 * Grad(delta, inPutN.Value) + 0.3 * 0;

        }

    }
    
    public static Neuron[][] GenerateNeurons(Neuron[][] neurons)
    {
        for (int i = 0; i < neurons.Length; i++)
        {
            for (int j = 0; j < neurons[i].Length; j++)
            {
                neurons[i][j] = new Neuron() {Id = id, Layer = i};
                id++;
                if (neurons.Length-1 > i)
                {
                    neurons[i][j].SynapsysConunt = neurons[i+1].Length;
                }
            }
        }
        for (int i = 0; i < neurons.Length; i++)
        {
            for (int j = 0; j < neurons[i].Length; j++)
            {
                //Console.Write($"{neurons[i][j].SynapsysConunt} || {neurons[i][j].Id}\n");
                for (int k = 0; k < neurons[i][j].SynapsysConunt; k++)
                {
                    Synapsys synaps = new Synapsys() {Weight = new Random().NextDouble() * (1 + 1) - 1, InputNeuron = neurons[i][j], OutputNeuron = neurons[i+1][k]};
                    neurons[i][j].Connections.Add(synaps);
                }
            }
        }
        return neurons;
    }

    public static Neuron ActivateNeurons(Neuron[][] neurons, Neuron neuron)
    {
        List<Synapsys> synapsysList = new List<Synapsys>();
        for (int j = 0; j < neurons[neuron.Layer-1].Length; j++)
        {
            synapsysList.AddRange(neurons[neuron.Layer - 1][j].Connections);
            
        }
        synapsysList= synapsysList.Where(N => N.OutputNeuron.Id == neuron.Id).ToList();
        double result = 0;
        foreach (var synaps in synapsysList)
        {
            var temp = synaps.InputNeuron.Value * synaps.Weight;
            result += temp;
        }
        result = LogSigmoid(result);
        neuron.Value = result;
        return neuron;
    }

    public static Neuron[][] StartActivation(Neuron[][] neurons)
    {
        for (int i = 1; i < neurons.Length; i++)
        {
            for (int j = 0; j < neurons[i].Length; j++)
            {
                neurons[i][j] = ActivateNeurons(neurons, neurons[i][j]);
            }
        }
        return neurons;
    }
}