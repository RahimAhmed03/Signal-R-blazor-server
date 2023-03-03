FROM mcr.microsoft.com/dotnet/sdk:7.0

EXPOSE 8080

COPY ./BlazorServerSignalRApp /BlazorServerSignalRApp
RUN dotnet publish -c Release --self-contained true -p:PublishSingleFile=true -o /app /BlazorServerSignalRApp

WORKDIR /app
RUN chmod +x ./BlazorServerSignalRApp
CMD ["./BlazorServerSignalRApp", "--urls", "http://0.0.0.0:8080"]