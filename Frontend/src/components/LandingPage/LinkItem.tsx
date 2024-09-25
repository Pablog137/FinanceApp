import { Link } from "react-router-dom";

interface LinkItemProps {
  path: string;
  text: string;
}

export default function LinkItem({ path, text }: LinkItemProps) {
  return (
    <Link to={path}>
      <button className="px-4 py-2 rounded-sm border hover:border-green-300 hover:border-2">
        {text}
      </button>
    </Link>
  );
}
