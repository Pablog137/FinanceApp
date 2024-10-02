import { useDarkMode } from "../../context/DarkModeContext";
import AsideElement from "./AsideElement";

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
        <AsideElement
          text="Dashboard"
          icon="fa-solid fa-home"
          textColor={textColor}
          url="/"
        />
        <AsideElement
          text="Notes"
          icon="fa-solid fa-sticky-note"
          textColor={textColor}
          url="/notes"
        />
      </ul>
    </aside>
  );
}
