﻿#ifndef GET_ROOF_DATA_HPP
#define GET_ROOF_DATA_HPP

#include "GetDataCommand.hpp"


namespace AddOnCommands {


class GetRoofData : public GetDataCommand {
	GS::String			GetFieldName () const override;
	API_ElemTypeID		GetElemTypeID () const override;
	GS::ErrCode			SerializeElementType (const API_Element& elem,
							const API_ElementMemo& memo,
							GS::ObjectState& os) const override;

public:
	virtual GS::String	GetName () const override;
};


}


#endif