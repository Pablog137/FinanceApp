import { useDarkMode } from "../../../context/DarkModeContext";
import AsideElementList from "./AsideElementList";

export default function AsideMobile() {
  const { isDarkMode, textColor } = useDarkMode();

  const bgColor = isDarkMode ? "dark:bg-gray-800" : "bg-white";
  const stylesLightMode = !isDarkMode && "opacity-90";

  return (
    <aside
      id="logo-sidebar"
      className={`${bgColor} fixed inset-x-0 bottom-0 w-full transition-transform sm:translate-x-0 flex-shrink-0 overflow-y-auto p-2 lg:hidden ${stylesLightMode}`}
      aria-label="Sidebar"
    >
      <ul className="font-medium list-none flex justify-center items-center">
        <AsideElementList textColor={textColor} />
      </ul>
    </aside>
  );
}
