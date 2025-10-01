import { Link } from "react-router-dom";
import type { IconLinkCellProps } from "../models/IconLinkCellProps";

const IconLinkCell = (props: IconLinkCellProps) => {
  const { to, icon, className = "link-cell" } = props;

  return (
    <Link to={to} className={className}>
      {icon}
    </Link>
  );
};

export default IconLinkCell;
