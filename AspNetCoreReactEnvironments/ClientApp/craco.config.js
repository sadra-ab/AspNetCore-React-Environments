const path = require("path");

const overrideWebpackConfig = (webpackConfig, context) => {

	if ((process.env.CLIENT_ENV || "production") == "staging") {
		context.paths.appBuild = webpackConfig.output.path = path.resolve(__dirname, "build", "staging");
	}
	else
		context.paths.appBuild = webpackConfig.output.path = path.resolve(__dirname, "build", "production");

	return webpackConfig;
};

module.exports = function ({ env }) {
	return {
		webpack: {
			configure: overrideWebpackConfig
		}
	}
}