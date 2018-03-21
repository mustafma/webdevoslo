FROM microsoft/aspnetcore:2.0
WORKDIR /app
EXPOSE 80

ENV Rabbit__Host="rabbit"
ENV Rabbit__Port="5672"
ENV ConnectionStrings__MyDbContext="Server=sql,1433;Database=demo;User Id=sa;Password=Sdm4ever!;MultipleActiveResultSets=true"
ENV QueueName="developer"

COPY bin/Release/netcoreapp2.0/linux-x64/publish  .

ENTRYPOINT ["dotnet", "web.dll"]
