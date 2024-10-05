import { useDarkMode } from "../../../context/DarkModeContext";
import AsideElement from "./AsideElement";
import AsideElementList from "./AsideElementList";

export default function Aside() {
  const { isDarkMode } = useDarkMode();

  const bgColor = isDarkMode ? "dark:bg-gray-800" : "bg-white";
  const textColor = isDarkMode ? "text-gray-200" : "text-gray-600";
  return (
    <aside
      id="logo-sidebar"
      className={`${bgColor} h-full min-h-screen transition-transform sm:translate-x-0 flex-shrink-0 overflow-y-auto`}
      aria-label="Sidebar"
    >
      <div className="flex flex-col justify-around md:px-3 xl:px-4 pb-4 pt-6">
        <ul className="font-medium flex flex-col items-center lg:items-start justify-start list-none flex-grow gap-1">
          <AsideElementList textColor={textColor} />
        </ul>
        <ul className="font-medium flex flex-col items-center lg:items-start justify-start list-none flex-grow gap-1">
          <AsideElement
            text="Settings"
            icon="fa-solid fa-gear"
            url="/settings"
            textColor={textColor}
            textSize="text-lg"
          />
        </ul>
      </div>
    </aside>
  );
}
