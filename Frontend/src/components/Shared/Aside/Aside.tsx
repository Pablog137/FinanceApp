import { useDarkMode } from "../../../context/DarkModeContext";
import AsideElementList from "./AsideElementList";

export default function Aside() {
  const { isDarkMode, textColor } = useDarkMode();

  const bgColor = isDarkMode ? "dark:bg-gray-800" : "bg-white";

  return (
    <aside
      id="logo-sidebar"
      className={`${bgColor} h-full min-h-screen transition-transform sm:translate-x-0 flex-shrink-0 overflow-y-auto`}
      aria-label="Sidebar"
    >
      <div className="h-full flex flex-col md:px-3 xl:px-4 pb-4 pt-6">
        <ul className="font-medium flex flex-col items-center lg:items-start justify-start list-none flex-grow">
          <AsideElementList textColor={textColor} />
        </ul>
      </div>
    </aside>
  );
}
