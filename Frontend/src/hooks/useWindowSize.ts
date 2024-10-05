import { useEffect, useState } from "react";

export default function useWindowSize() {
  const [windowWidth, setWindowWidth] = useState(window.innerWidth);

  const handleResize = () => {
    setWindowWidth(window.innerWidth);
  };

  useEffect(() => {
    window.addEventListener("resize", handleResize);
    return () => window.removeEventListener("resize", handleResize);
  }, []);

  const isMobile = windowWidth < 768;
  const chartWidth = windowWidth < 900 ? 600 : 800;
  const chartHeight = windowWidth < 900 ? 300 : 400;

  return {
    windowWidth,
    isMobile,
    chartSize: chartWidth,
    chartHeight,
  };
}
