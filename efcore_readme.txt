# after changing the model
dotnet ef migrations add <name i want to give the change>

#the above creates a new migration

# to update the local dev database based in the migrations
dotnet ef database update

#to drop the local dev database
dotnet ef database drop