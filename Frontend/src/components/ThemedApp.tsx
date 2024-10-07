import { Route, Routes } from "react-router";
import LandingPage from "../pages/LandingPage";
import Login from "../pages/Login";
import Register from "../pages/Register";
import { useDarkMode } from "../context/DarkModeContext";
import NotFound from "../pages/NotFound";
import Dashboard from "../pages/Dashboard";
import Transfer from "../pages/Transfer";

const ThemedApp = () => {
  const { isDarkMode } = useDarkMode();

  const bgColor = isDarkMode ? "dark" : "white";

  return (
    <div className={`${bgColor} h-screen`}>
      <Routes>
        <Route path="/" element={<LandingPage />} />
        <Route path="/login" element={<Login />} />
        <Route path="/register" element={<Register />} />
        <Route path="/dashboard" element={<Dashboard />} />
        <Route path="/transfer" element={<Transfer />} />
        <Route path="*" element={<NotFound />} />
      </Routes>
    </div>
  );
};

export default ThemedApp;
