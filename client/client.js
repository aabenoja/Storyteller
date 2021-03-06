// Array.prototype.findIndex - MIT License (c) 2013 Paul Miller <http://paulmillr.com>
// For all details and docs: <https://github.com/paulmillr/Array.prototype.findIndex>
(function (globals) {
  if (Array.prototype.findIndex) return;

  var findIndex = function(predicate) {
    var list = Object(this);
    var length = Math.max(0, list.length) >>> 0; // ES.ToUint32;
    if (length === 0) return -1;
    if (typeof predicate !== 'function' || Object.prototype.toString.call(predicate) !== '[object Function]') {
      throw new TypeError('Array#findIndex: predicate must be a function');
    }
    var thisArg = arguments.length > 1 ? arguments[1] : undefined;
    for (var i = 0; i < length; i++) {
      if (predicate.call(thisArg, list[i], i, list)) return i;
    }
    return -1;
  };

  if (Object.defineProperty) {
    try {
      Object.defineProperty(Array.prototype, 'findIndex', {
        value: findIndex, configurable: true, writable: true
      });
    } catch(e) {}
  }

  if (!Array.prototype.findIndex) {
    Array.prototype.findIndex = findIndex;
  }
}(this));

var Postal = require('postal');
var Communicator = require('./lib/communicator');

var startRouting = require('./components/app');

require('./lib/stores/hierarchy').reset(); // just forcing it to get loaded

Postal.publish({
  channel: 'engine',
  topic: 'system-recycled',
  data: Storyteller.initialization
});


var ResultCache = require('./lib/stores/result-cache');
var results = JSON.parse(_.unescape(document.getElementById('result-data').innerHTML));
(results || []).forEach(x => {
  ResultCache.record(x);
});

var hierarchy = JSON.parse(_.unescape(document.getElementById('hierarchy-data').innerHTML));
Postal.publish({
	channel: 'engine',
	topic: 'hierarchy-loaded',
	data: {hierarchy: hierarchy}
});




Postal.publish({
  channel: 'engine',
  topic: 'queue-state',
  data: Storyteller.queueState
});


var disconnect = require('./components/disconnected');
var communicator = new Communicator(Storyteller.wsAddress, startRouting, disconnect);



Postal.subscribe({
	channel: 'explorer',
	topic: 'go-to-spec',
	callback: function(data){
		var href = '#/spec/editing/' + data.id;
		window.location = href;
	}
});