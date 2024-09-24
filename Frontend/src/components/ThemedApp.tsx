import { Route, Routes } from "react-router";
import LandingPage from "../pages/LandingPage";
import Login from "../pages/Login";
import Register from "../pages/Register";
import { useDarkMode } from "../context/DarkModeContext";
import NotFound from "../pages/NotFound";

const ThemedApp = () => {
  const { isDarkMode } = useDarkMode();

  const bgColor = isDarkMode ? "dark" : "white";

  return (
    <div className={`${bgColor} h-screen p-2`}>
      <Routes>
        <Route path="/" element={<LandingPage />} />
        <Route path="/login" element={<Login />} />
        <Route path="/register" element={<Register />} />
        <Route path="*" element={<NotFound />} />
      </Routes>
    </div>
  );
};

export default ThemedApp;
