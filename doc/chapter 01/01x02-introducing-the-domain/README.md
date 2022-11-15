# 01x02 - Introducing the Domain

## A look over the system

The View Model contains now a lot of logic and decisions that are not its responsibility to make:

- Data Access Logic

  - For example, it loads the inflation data from the JSON file. The data must be loaded, but is its responsibility (the View Model, part of the presentation layer) to know from where it should be loaded? Or when it should be loaded?
    ```csharp
    private static List<Inflation> LoadInflations()
    {
        Assembly assembly = Assembly.GetExecutingAssembly();
        using Stream stream = assembly.GetManifestResourceStream("...inflation-yearly.json");
        using StreamReader streamReader = new(stream);
        string json = streamReader.ReadToEnd();
        return JsonConvert.DeserializeObject<List<Inflation>>(json);
    }
    ```

- Business Logic

  - Same situation we have with the calculation of the output value. The value must be calculated, but it is not the responsibility of the presentation layer to actually implement the algorithm.
    ```csharp
    private void CalculateOutputValue()
    {
        ...
        ...
        ...
        ...
    
        OutputValue = calculatedValue;
    }
    ```

    

Having these responsibilities separated in their own components, presents some advantages. Let's take them one by one and discuss about them.

## Business Logic

Why is it useful to have business logic separated in its own component?

Uncle Bob is talks about two distinct types of what I was previously calling Business Logic:

- **Critical Business Rules** (Domain)
  - This logic is placed in classes called Entities, in the Domain component.
  - It is the most important logic of the application. This is the reason the application exists. If this logic would not exist or it was buggy, the application would become useless.
  - It is useful to isolate this logic in its own component so that we do not have to touch it and risk damage it when we work at other parts of the application, like data access or presentation.
- **Application-Specific Business Rules** (Use Cases)
  - This logic is placed in the so called Use Cases and extracted in the Application component.
  - Each user action triggers the execution of a Use Cases and it decides when, what and in which order to execute the Entities. "Use cases control the dance of the Entities", as Uncle Bob describes them.

## Domain Logic

First think that we can do is to extract the `CalculateOutputValue()` method in a separate class and, then, in a separate project called Domain.

```csharp
public class Calculator
{
    public List<Inflation> Inflations { get; set; }

    public float InputValue { get; set; }

    public float? OutputValue { get; set; }

    public string StartTime { get; set; }

    public string EndTime { get; set; }

    public void Calculate()
    {
        int startIndex = Inflations.FindIndex(x => x.Time == StartTime);

        int endIndex = Inflations.FindIndex(x => x.Time == EndTime);

        if (startIndex == -1 || endIndex == -1)
        {
            OutputValue = null;
        }
        else
        {
            float calculatedValue = InputValue;

            if (startIndex < endIndex)
            {
                for (int i = startIndex + 1; i <= endIndex; i++)
                {
                    float inflationRate = Inflations[i].InflationRate;
                    float inflationValue = 1 + inflationRate / 100;
                    calculatedValue *= inflationValue;
                }
            }
            else if (startIndex > endIndex)
            {
                for (int i = startIndex; i > endIndex; i--)
                {
                    float inflationRate = Inflations[i].InflationRate;
                    float inflationValue = 1 + inflationRate / 100;
                    calculatedValue /= inflationValue;
                }
            }

            OutputValue = calculatedValue;
        }
    }
}
```

The View Model will instantiate and execute this class:

```csharp
private void CalculateOutputValue()
{
    Calculator calculator = new()
    {
        Inflations = inflations,
        InputValue = inputValue,
        StartTime = selectedStartTime,
        EndTime = selectedEndTime
    };

    calculator.Calculate();
    OutputValue = calculator.OutputValue;
}
```

Note: Also the `Inflation` class must be moved into the Domain component. It is used by the `Calculator`.
