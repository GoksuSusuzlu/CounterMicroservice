<!DOCTYPE html>
<html lang="en">
<head>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>CounterMicroservice</title>
</head>
<body>
    <h1>CounterMicroservice</h1>
    <h2>Overview</h2>
    <p>The CounterMicroservice is a microservice responsible for counting and tracking various metrics. It is developed using .NET and follows the Clean Architecture principles.</p>
    <h2>Prerequisites</h2>
    <ul>
        <li>.NET 7 SDK</li>
        <li>Docker</li>
    </ul>
    <h2>Getting Started</h2>
    <h3>Clone the Repository</h3>
    <pre>
<code>
git clone https://github.com/GoksuSusuzlu/CounterMicroservice.git
cd CounterMicroservice
</code>
    </pre>
    <h3>Configuration</h3>
    <p>Update the <code>appsettings.json</code> file in the project with the appropriate settings for your environment.</p>
    <h3>Run the Application</h3>
    <h4>Using .NET CLI</h4>
    <ol>
        <li>Navigate to the project directory:
            <pre><code>cd src/CounterMicroservice</code></pre>
        </li>
        <li>Restore dependencies and run the application:
            <pre><code>
dotnet restore
dotnet run
            </code></pre>
        </li>
    </ol>
    <h4>Using Docker</h4>
    <ol>
        <li>Build the Docker image:
            <pre><code>docker build -t counter-microservice .</code></pre>
        </li>
        <li>Run the Docker container:
            <pre><code>docker run -d -p 5001:80 counter-microservice</code></pre>
        </li>
    </ol>
    <h3>Testing</h3>
    <p>To run the tests, navigate to the test project directory and use the .NET CLI:</p>
    <pre><code>
cd tests/CounterMicroservice.Tests
dotnet test
    </code></pre>
    <h2>License</h2>
    <p>This project is licensed under the MIT License.</p>
</body>
</html>
