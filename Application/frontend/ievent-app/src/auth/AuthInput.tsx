import React, { useState } from 'react';

const AuthInput: React.FC = () => {
    const [email, setEmail] = useState('');
    const [password, setPassword] = useState('');
    const [isRegister, setIsRegister] = useState(false);
    const [isForgotPassword, setIsForgotPassword] = useState(false);

    const handleSubmit = (event: React.FormEvent) => {
        event.preventDefault();
        if (isForgotPassword) {
            // Handle forgot password logic
            console.log('Forgot Password:', email);
        } else if (isRegister) {
            // Handle registration logic
            console.log('Register:', email, password);
        } else {
            // Handle login logic
            console.log('Login:', email, password);
        }
    };

    return (
        <div className="flex flex-col items-center justify-center min-h-screen bg-gray-100">
            <div className="w-full max-w-md p-8 space-y-6 bg-white rounded-lg shadow-md">
                <h2 className="text-2xl font-bold text-center text-gray-800">
                    {isForgotPassword ? 'Reset Password' : isRegister ? 'Register' : 'Login'}
                </h2>
                <form onSubmit={handleSubmit} className="space-y-6">
                    <div>
                        <label className="block text-sm font-medium text-gray-700">Email:</label>
                        <input
                            type="email"
                            value={email}
                            onChange={(e) => setEmail(e.target.value)}
                            required
                            className="w-full px-3 py-2 mt-1 border border-gray-300 rounded-md shadow-sm focus:outline-none focus:ring-indigo-500 focus:border-indigo-500"
                        />
                    </div>
                    {!isForgotPassword && (
                        <div>
                            <label className="block text-sm font-medium text-gray-700">Password:</label>
                            <input
                                type="password"
                                value={password}
                                onChange={(e) => setPassword(e.target.value)}
                                required
                                className="w-full px-3 py-2 mt-1 border border-gray-300 rounded-md shadow-sm focus:outline-none focus:ring-indigo-500 focus:border-indigo-500"
                            />
                        </div>
                    )}
                    <button
                        type="submit"
                        className="w-full px-4 py-2 text-white bg-indigo-600 rounded-md hover:bg-indigo-700 focus:outline-none focus:ring-2 focus:ring-offset-2 focus:ring-indigo-500"
                    >
                        {isForgotPassword ? 'Reset Password' : isRegister ? 'Register' : 'Login'}
                    </button>
                </form>
                <div className="flex justify-between">
                    {!isForgotPassword && (
                        <button
                            onClick={() => setIsRegister(!isRegister)}
                            className="text-sm text-indigo-600 hover:underline"
                        >
                            {isRegister ? 'Switch to Login' : 'Switch to Register'}
                        </button>
                    )}
                    <button
                        onClick={() => setIsForgotPassword(!isForgotPassword)}
                        className="text-sm text-indigo-600 hover:underline"
                    >
                        {isForgotPassword ? 'Back to Login' : 'Forgot Password'}
                    </button>
                </div>
            </div>
        </div>
    );
};

export default AuthInput;