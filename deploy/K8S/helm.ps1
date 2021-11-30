param (
    [Parameter()]
    [switch]
    $uninstall
)

if ($uninstall) {
    write-host "Uninstalling dapr" -ForegroundColor Red
    helm uninstall dapr --namespace dapr-system

    write-host "Uninstalling rabbitmq" -ForegroundColor Red
    helm uninstall rabbitmq
    kubectl delete secret/rabbitmqconnectionstring

    write-host "Uninstalling redis" -ForegroundColor Red
    helm uninstall redis
    
}
else {
    write-host "Installing dapr" -ForegroundColor Green
    helm install dapr dapr/dapr --version=1.5 --namespace dapr-system --create-namespace --wait
    
    write-host "Installing rabbitmq" -ForegroundColor Green
    helm install rabbitmq --set rabbitmq.username=admin,rabbitmq.password=secretpassword,rabbitmq.erlangCookie=secretcookie bitnami/rabbitmq

    $EncryptedPassword = (kubectl get secret --namespace default rabbitmq -o jsonpath="{.data.rabbitmq-password}")
    $PASSWORD = [System.Text.Encoding]::UTF8.GetString([System.Convert]::FromBase64String($EncryptedPassword))
    $RabbitMQConnnection = "amqp://user:$PASSWORD@rabbitmq:5672"
    kubectl create secret generic rabbitmqconnectionstring --from-literal=host=$RabbitMQConnnection
    
    write-host "Installing redis" -ForegroundColor Green
    helm install redis bitnami/redis
}
