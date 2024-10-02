import { useDarkMode } from "../../../context/DarkModeContext";
import AsideElementList from "./AsideElementList";

export default function AsideMobile() {
  const { isDarkMode, textColor } = useDarkMode();

  const bgColor = isDarkMode ? "dark:bg-gray-800" : "bg-white";

  return (
    <aside
      id="logo-sidebar"
      className={`${bgColor} col-span-12 w-full transition-transform sm:translate-x-0 flex-shrink-0 overflow-y-auto p-2`}
      aria-label="Sidebar"
    >
      <ul className="font-medium list-none flex justify-center items-center">
        <AsideElementList textColor={textColor} />
      </ul>
    </aside>
  );
}
