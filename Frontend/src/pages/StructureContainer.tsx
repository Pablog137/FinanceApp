import { useEffect, useState } from "react";
import Aside from "../components/Shared/Aside/Aside";
import Navbar from "../components/Shared/NavBar/NavBar";
import NavBarMobile from "../components/Shared/NavBar/NavBarMobile";
import AsideMobile from "../components/Shared/Aside/AsideMobile";
import { useDarkMode } from "../context/DarkModeContext";

interface StructureContainerProps {
  MainComponent: any;
}

export default function StructureContainer({
  MainComponent,
}: StructureContainerProps) {
  const [isAsideOpen, setIsAsideOpen] = useState(false);
  const [windowWidth, setWindowWidth] = useState(window.innerWidth);
  const { isDarkMode } = useDarkMode();

  const toggleAside = () => {
    setIsAsideOpen(!isAsideOpen);
  };

  const handleResize = () => {
    setWindowWidth(window.innerWidth);
  };

  useEffect(() => {
    window.addEventListener("resize", handleResize);
    return () => window.removeEventListener("resize", handleResize);
  }, []);

  const colsAside = isAsideOpen ? "col-span-1 md:col-span-2" : "hidden";
  const colMain = isAsideOpen ? "md:col-span-10 col-span-11" : "col-span-12";
  const isMobile = windowWidth < 768;
  const bgColor = isDarkMode ? "bg-[#161922]" : "bg-[rgb(246,246,246)]";

  return (
    <>
      {isMobile ? (
        <>
          <NavBarMobile />
          <div className={`${bgColor} grid grid-cols-12 `}>
            <MainComponent colMain="col-span-12" />
          </div>
          <AsideMobile />
        </>
      ) : (
        <>
          <Navbar isAsideOpen={isAsideOpen} toggleAside={toggleAside} />
          <div className={`${bgColor} grid grid-cols-12`}>
            <div className={colsAside}>
              <Aside />
            </div>
            <MainComponent colMain={colMain} />
          </div>
        </>
      )}
    </>
  );
}
