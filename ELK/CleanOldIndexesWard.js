var elasticsearch = require('elasticsearch');
var indexName = 'logstash-wardfile';
var client = new elasticsearch.Client({
  host: 'localhost:9200',
  log:   [{
	  type: 'stdio',
	  levels: ['error', 'warning']
	}]
});

client.ping({
  requestTimeout: 30000,

  // undocumented params are appended to the query string
  hello: "elasticsearch!"
}, function (error) {
  if (error) {
    console.error('elasticsearch cluster is down!');
  } else {
    console.log('All is well');
  }
});

client.indices.delete({
  index: indexName,
  ignore: [404]
}).then(function (body) {
  // since we told the client to ignore 404 errors, the
  // promise is resolved even if the index does not exist
  console.log(indexName + ' index was deleted or never existed');
}, function (error) {
  console.log(indexName + ' index could not be deleted! ' + error);
});
