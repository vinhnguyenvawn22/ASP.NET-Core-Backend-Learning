double[] scores = { 7.5, 8.0, 6.5, 9.0, 5.5 };

double total_score = 0;
int count_score = 0;
for (int i = 0; i < scores.Length; i++)
{
    Console.WriteLine($"Index {i}: {scores[i]}");
    total_score += scores[i];
}
foreach (double score in scores)
{
    if (score >= 8)
    {
        count_score += 1;
    }
}

double average = total_score / scores.Length;
Console.WriteLine($"Total: {total_score}");
Console.WriteLine($"Average: {average}");
Console.WriteLine($"Score >=8.0: {count_score}");
