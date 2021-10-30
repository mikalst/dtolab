provider "azurerm" {
    features {}
}

resource "azurerm_resource_group" "example" {
  name     = "rg-json2record"
  location = "westeurope"
}

###############
### BACKEND ###
###############
resource "azurerm_app_service_plan" "example" {
  name                = "plan-json2record"
  location            = azurerm_resource_group.example.location
  resource_group_name = azurerm_resource_group.example.name

  sku {
    tier = "Dynamic"
    size = "Y1"
  }
}

resource "azurerm_storage_account" "example" {
  name                     = "stjson2record"
  resource_group_name      = azurerm_resource_group.example.name
  location                 = azurerm_resource_group.example.location
  account_tier             = "Standard"
  account_replication_type = "LRS"
}

resource "azurerm_function_app" "example" {
  name                       = "func-json2record"
  location                   = azurerm_resource_group.example.location
  resource_group_name        = azurerm_resource_group.example.name
  app_service_plan_id        = azurerm_app_service_plan.example.id
  storage_account_name       = azurerm_storage_account.example.name
  storage_account_access_key = azurerm_storage_account.example.primary_access_key
}

################
### FRONTEND ###
################
resource "azurerm_static_site" "example" {
  name                = "swa-json2record"
  resource_group_name = azurerm_resource_group.example.name 
  location            = azurerm_resource_group.example.location
  sku_tier            = "Free"
  sku_size            = "Free"
}