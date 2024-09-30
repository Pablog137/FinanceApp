import { useDarkMode } from "../context/DarkModeContext";

export default function Dashboard() {
  const { textColor } = useDarkMode();

  return <div className={`${textColor} text-3xl`}>Dashboard</div>;
}
