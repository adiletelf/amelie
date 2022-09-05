const { createProxyMiddleware } = require('http-proxy-middleware');

const context = [
    "/api/author",
    "/api/book",
];

module.exports = function (app) {
    const appProxy = createProxyMiddleware(context, {
        target: 'https://localhost:5001',
        secure: false
    });

    app.use(appProxy);
};
