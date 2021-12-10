param (
    [Parameter()]
    [switch]
    $Login,
    [switch]
    $SetAccount,
    [switch]
    $LoginACR,
    [string]
    $ACRName = "eshopdevconf",
    [switch]
    $PullSecret,
    [switch]
    $CreateServicePrincipal,
    [switch]
    $AKSCredential,
    [string]
    $PrincipalName = "acr-service-principal"
)
if($SetAccount)
{
    az account set --subscription $SubscriptionID
}
elseif ($Login) 
{
    az login
}
elseif ($LoginACR) 
{
    az acr login --name $ACRName
}
elseif ($PullSecret) 
{
    $ACR_REGISTRY_ID=$(az acr show --name $ACRName --query "id" --output tsv)

    if ($CreateServicePrincipal)
    {
        
        $PASSWORD=$(az ad sp create-for-rbac --name $PrincipalName --scopes $ACR_REGISTRY_ID --role acrpush --query "password" --output tsv)
        $USER_NAME=$(az ad sp list --display-name $PrincipalName --query "[].appId" --output tsv)
    }
    else 
    {
        
        $USER_NAME=$(az ad sp list --display-name $PrincipalName --query "[].appId" --output tsv)
        az role assignment create --assignee $USER_NAME --scope $ACR_REGISTRY_ID --role acrpush
        $PASSWORD=$(az ad sp credential reset --name $PrincipalName --query "password" --output tsv)
    }
    kubectl delete secret/acr-secret
    kubectl create secret docker-registry acr-secret --docker-server=$ACRName.azurecr.io --docker-username=$USER_NAME --docker-password=$PASSWORD
}
elseif($AKSCredential)
{
    az aks get-credentials --resource-group DevConf --name DevConf2022
}