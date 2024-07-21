import { createBrowserRouter, Navigate } from "react-router-dom";
import App from "../App";
import HomePage from "../Pages/HomePage/HomePage";
import ProtectedRoute from "./ProtectedRoute";

export const router = createBrowserRouter([
    {
        path: "/",
        element: <App />,
        children: [
            { path: "", element: <HomePage /> },
            {
                path: "recipes",
                element: (
                    <ProtectedRoute>
                        <HomePage />
                    </ProtectedRoute>
                )
            },
            {
                path: "favourites",
                element: (
                    <ProtectedRoute>
                        <HomePage />
                    </ProtectedRoute>
                )
            },
            { path: "*", element: <Navigate to="/" replace /> }
        ]
    }
])