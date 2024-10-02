import { useEffect, useState } from "react";
import Aside from "../components/Shared/Aside";
import Navbar from "../components/Shared/NavBar";
import NavBarMobile from "../components/Shared/NavBarMobile";
import AsideMobile from "../components/Shared/AsideMobile";

interface StructureContainerProps {
  MainComponent: any;
}

export default function StructureContainer({
  MainComponent,
}: StructureContainerProps) {
  const [isAsideOpen, setIsAsideOpen] = useState(true);
  const [windowWidth, setWindowWidth] = useState(window.innerWidth);

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

  return (
    <>
      {isMobile ? (
        <>
          <NavBarMobile />
          <div className="grid grid-cols-12">
            <MainComponent colMain="col-span-12" />
            <AsideMobile />
          </div>
        </>
      ) : (
        <>
          <Navbar isAsideOpen={isAsideOpen} toggleAside={toggleAside} />
          <div className="grid grid-cols-12">
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
