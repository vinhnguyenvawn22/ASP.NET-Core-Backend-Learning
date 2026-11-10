using CancellationTokenSource cts =
    new CancellationTokenSource();

cts.CancelAfter(2000);

try
{
    await DownloadAsync(cts.Token);
}
catch (OperationCanceledException)
{
    Console.WriteLine("Da huy tai du lieu!");
}

static async Task DownloadAsync(
    CancellationToken token)
{
    Console.WriteLine("Bat dau tai...");

    await Task.Delay(5000, token);

    Console.WriteLine("Tai thanh cong!");
}