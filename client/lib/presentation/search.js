var Hierarchy = require('./../stores/hierarchy');
var _ = require('lodash');

function SuiteOption(suite){
	this.text = suite.path.toLowerCase();
	this.title = 'Suite: ' + suite.path;
	this.url = '/#/suite/' + suite.path;
}

function SpecOption(spec){
	this.text = spec.title.toLowerCase();
	this.title = 'Spec: ' + spec.title;
	this.url = '/#/spec/editing/' + spec.id;
}

module.exports = {
	findMatches(query){
		var top = Hierarchy.top();

		var suites = top.allSuites().map(x => new SuiteOption(x));
		var specs = top.allSpecs().map(x => new SpecOption(x));

		var all = _.sortBy(suites.concat(specs), x => x.text);

		var fragment = query.toLowerCase();

		return all.filter(x => {
			return x.text.search(fragment) > -1;
		});
	}
}