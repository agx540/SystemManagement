var agProvider = angular.module('agProvider',['ngResource']
	
);


agProvider.factory('providerService', function($resource){
	return $resource('/Provider');
});

agProvider.controller('providerController', function(providerService, $scope, $rootScope){
	$scope.providers = providerService.query();
	
  $scope.setValue = function(provider){
    $scope.selectedProvider = provider;
  }

});

//agProvider.Config
//agProvider.run
//agProvider.provider
//agProvider.factory
//agProvider.service
//agProvider.value
//agProvider.constant
//agProvider.decorator
//agProvider.animation
//agProvider.filter
//agProvider.controller
//agProvider.directive
//agProvider.config
//agProvider.run

