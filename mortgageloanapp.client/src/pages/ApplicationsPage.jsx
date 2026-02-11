import { useEffect, useState } from 'react';
import { getApplications } from '../api/mortgageApi';
import './ApplicationsPage.css';

export default function ApplicationsPage() {
    const [applications, setApplications] = useState([]);
    const [error, setError] = useState(null);
    const formatCurrency = (amount) =>
        new Intl.NumberFormat('en-GB', {
            style: 'currency',
            currency: 'GBP'
        }).format(amount);

    useEffect(() => {
        getApplications()
            .then(setApplications)
            .catch(err => setError(err.message));
    }, []);

    if (error) {
        return <p>Error: {error}</p>;
    }

    return (
        <div>
            <h1>Mortgage Applications</h1>

            <table>
                <tr>
                    <th>Applicant Name</th>
                    <th>Loan Amount</th>
                    <th>Loan Status</th>
                </tr>
                {applications.map(app => (
                    <tr key={app.id}>
                        <td>{app.applicantName}</td>
                        <td>{formatCurrency(app.loanAmount)}</td>
                        <td>{app.loanStatus}</td>
                    </tr>
                ))}
            </table>
        </div>
    );
}