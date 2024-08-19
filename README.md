# azurefunction
Azure function bindings

RESOURCE_GROUP="rg-az-204"
NAMESPACE_NAME="Az204NamespaceName"
QUEUE_NAME="TopicOrQueueName1"
LOCATION="eastus"
STORAGE_ACCOUNT_NAME="az204strgacc"

# Create a resource group
az group create --name $RESOURCE_GROUP --location $LOCATION

# Create a Service Bus namespace
az servicebus namespace create --resource-group $RESOURCE_GROUP --name $NAMESPACE_NAME --location $LOCATION

# Create a Service Bus queue
az servicebus queue create --resource-group $RESOURCE_GROUP --namespace-name $NAMESPACE_NAME --name $QUEUE_NAME

# Create a storage account
az storage account create --name $STORAGE_ACCOUNT_NAME --resource-group $RESOURCE_GROUP --location $LOCATION --sku Standard_LRS --kind StorageV2

# Create a blob container
CONTAINER_NAME="az204conatiner"
az storage container create --name $CONTAINER_NAME --account-name $STORAGE_ACCOUNT_NAME