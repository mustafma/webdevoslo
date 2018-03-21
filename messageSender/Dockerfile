FROM microsoft/aspnetcore:2.0
WORKDIR /app

ENV ConnectionStrings__MyDbContext="data source=;Database=;User Id=;Password=;MultipleActiveResultSets=true"
ENV Rabbit__Host="rabbit"
ENV Rabbit__Port="5672"
ENV QueueName="developer"

COPY bin/Release/netcoreapp2.0/linux-x64/publish  .

ENTRYPOINT ["dotnet", "messageSender.dll"]
